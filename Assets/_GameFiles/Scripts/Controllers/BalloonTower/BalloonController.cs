using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public class BalloonController : MonoBehaviour
    {
        [Header("Balloon Settings")] 
        public Transform target;
        [SerializeField] private float accelerationMultiplier;
        [SerializeField] private float turnSpeed;

        [Header("Ray Variables")]
        private List<Transform> _heliumPositions = new List<Transform>();
        private RaycastHit[] _hits = new RaycastHit[4];

        [Header("Rotation Variables")] 
        private Quaternion _rotGoal;
        private Vector3 _direction;

        [HideInInspector]
        public Material balloonMat;
        
        private Rigidbody _rigidbody;
        
    
        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            balloonMat = GetComponent<Renderer>().material;
            
            foreach (Transform child in transform)
            {
                _heliumPositions.Add(child);
            }
        }

        private void Update()
        {
            Vector3 position = transform.position;
            _direction = (target.position - position).normalized;

            _rotGoal = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, _rotGoal, turnSpeed);
        }

        void FixedUpdate()
        {
            for (int i = 0; i < 4; i++)
            {
                Acceleration(_heliumPositions[i], _hits[i]);
            }
        }

        void Acceleration(Transform heliumTransform, RaycastHit hit)
        {
            Vector3 direction = Quaternion.AngleAxis(180, heliumTransform.forward) * -heliumTransform.up;
            
            if (Physics.Raycast(heliumTransform.position, direction, out hit, 15, 1 << 6))
            {
                Debug.Log("VAR");
                Vector3 position = heliumTransform.position;
                float force = Mathf.Abs(1 / (hit.point.y - position.y));
                
                _rigidbody.AddForceAtPosition(transform.forward * (force * accelerationMultiplier), position, ForceMode.Acceleration);
            }
        }
    }
}