using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public class RopeController : MonoBehaviour
    { 
        [Header("Holders")]
        public Transform holdingHand; 
        public BalloonController balloon;
        
        [Header("Rope Settings")]
        [SerializeField] private float ropePointDistance; 
        [SerializeField] private int ropePointAmount; 
        [SerializeField] private float ropeLineWidth;
        
        private LineRenderer _lineRenderer; 
        private List<RopePoint> _ropePoints = new List<RopePoint>(); 

        
        void Start() 
        { 
            _lineRenderer = GetComponent<LineRenderer>(); 
            Vector3 ropeStartPoint = holdingHand.position;
            
            for (int i = 0; i < ropePointAmount; i++) 
            {
                _ropePoints.Add(new RopePoint(ropeStartPoint));
                ropeStartPoint.y -= ropePointDistance;
            }
        }
        void Update()
        {
            CreateRope();
        }

        private void FixedUpdate()
        {
            Simulate();
        }

        private void Simulate()
        {
            Vector3 gravity = new Vector3(0f, -1f);

            for (int i = 1; i < ropePointAmount; i++)
            {
                RopePoint firstPoint = _ropePoints[i];
                
                Vector3 velocity = firstPoint.PosCurrent - firstPoint.PosOld;
                
                firstPoint.PosOld = firstPoint.PosCurrent;
                firstPoint.PosCurrent += velocity;
                firstPoint.PosCurrent += gravity * Time.fixedDeltaTime;
                
                _ropePoints[i] = firstPoint;
            }

            for (int i = 0; i < 50; i++)
            {
                ApplyConstraint();
            }
        }

        private void ApplyConstraint()
        {
            RopePoint firstPoint = _ropePoints[0];
            firstPoint.PosCurrent = holdingHand.position;
            _ropePoints[0] = firstPoint;


            RopePoint balloonPoint = _ropePoints[_ropePoints.Count - 1];
            balloonPoint.PosCurrent = balloon.transform.position;
            _ropePoints[_ropePoints.Count - 1] = balloonPoint;

            for (int i = 0; i < ropePointAmount - 1; i++)
            {
                RopePoint firstRopePoint = _ropePoints[i];
                RopePoint secondRopePoint = _ropePoints[i + 1];

                float distance = (firstRopePoint.PosCurrent - secondRopePoint.PosCurrent).magnitude;
                float errorDistance = Mathf.Abs(distance - ropePointDistance);
                
                Vector3 changeDirection = Vector3.zero;

                if (distance > ropePointDistance)
                {
                    changeDirection = (firstRopePoint.PosCurrent - secondRopePoint.PosCurrent).normalized;
                }
                else if (distance < ropePointDistance)
                {
                    changeDirection = (secondRopePoint.PosCurrent - firstRopePoint.PosCurrent).normalized;
                }

                Vector3 changeAmount = changeDirection * errorDistance;
                
                if (i != 0)
                {
                    firstRopePoint.PosCurrent -= changeAmount * 0.5f;
                    _ropePoints[i] = firstRopePoint;
                    
                    secondRopePoint.PosCurrent += changeAmount * 0.5f;
                    _ropePoints[i + 1] = secondRopePoint;
                }
                else
                {
                    secondRopePoint.PosCurrent += changeAmount;
                    _ropePoints[i + 1] = secondRopePoint;
                }
            }
        }

        private void CreateRope()
        {
            _lineRenderer.startWidth = ropeLineWidth;
            _lineRenderer.endWidth = ropeLineWidth;

            Vector3[] ropePositions = new Vector3[ropePointAmount];
            for (int i = 0; i < ropePointAmount; i++)
            {
                ropePositions[i] = _ropePoints[i].PosCurrent;
            }

            _lineRenderer.positionCount = ropePositions.Length;
            _lineRenderer.SetPositions(ropePositions);
        }

    
    }
}