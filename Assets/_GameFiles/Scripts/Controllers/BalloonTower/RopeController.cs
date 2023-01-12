using System;
using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public class RopeController : MonoBehaviour
    { 
        [Header("Holders")]
        [SerializeField] private Transform holdingHand; 
        [SerializeField] private Transform balloon;
        
        [Header("Rope Settings")]
        [SerializeField] private float ropePointDistance = 0.25f; 
        [SerializeField] private int ropePointAmount = 35; 
        [SerializeField] private float ropeLineWidth = 0.1f;
        
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
            DrawRope();
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
                this.ApplyConstraint();
            }
        }

        private void ApplyConstraint()
        {
            RopePoint firstPoint = _ropePoints[0];
            firstPoint.PosCurrent = holdingHand.position;
            _ropePoints[0] = firstPoint;


            RopePoint balloonPoint = _ropePoints[_ropePoints.Count - 1];
            balloonPoint.PosCurrent = balloon.position;
            _ropePoints[_ropePoints.Count - 1] = balloonPoint;

            for (int i = 0; i < ropePointAmount - 1; i++)
            {
                RopePoint firstSeg = _ropePoints[i];
                RopePoint secondSeg = _ropePoints[i + 1];

                float dist = (firstSeg.PosCurrent - secondSeg.PosCurrent).magnitude;
                float error = Mathf.Abs(dist - ropePointDistance);
                Vector3 changeDir = Vector3.zero;

                if (dist > ropePointDistance)
                {
                    changeDir = (firstSeg.PosCurrent - secondSeg.PosCurrent).normalized;
                }
                else if (dist < ropePointDistance)
                {
                    changeDir = (secondSeg.PosCurrent - firstSeg.PosCurrent).normalized;
                }

                Vector3 changeAmount = changeDir * error;
                if (i != 0)
                {
                    firstSeg.PosCurrent -= changeAmount * 0.5f;
                    _ropePoints[i] = firstSeg;
                    secondSeg.PosCurrent += changeAmount * 0.5f;
                    _ropePoints[i + 1] = secondSeg;
                }
                else
                {
                    secondSeg.PosCurrent += changeAmount;
                    _ropePoints[i + 1] = secondSeg;
                }
            }
        }

        private void DrawRope()
        {
            float lineWidth = ropeLineWidth;
            _lineRenderer.startWidth = lineWidth;
            _lineRenderer.endWidth = lineWidth;

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