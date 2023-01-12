using System.Collections.Generic;
using _GameFiles.Scripts.Controllers.BalloonTower;
using _GameFiles.Scripts.Utilities;
using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers.BalloonTower
{
    public class BalloonTowerLevelManager : BaseManager
    {
        [Header("Rope Controller Pool Settings")]
        [SerializeField] private RopeController ropeControllerPrefab;
        [SerializeField] private int maxRopeController;
        private Queue<RopeController> _ropeControllersQueue = new Queue<RopeController>();

        [SerializeField] private Transform target;
        [SerializeField] private List<RopeController> ropeControllers = new List<RopeController>();
        [SerializeField] private Transform holdingHand;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case AddBalloonButtonClickedEventArgs addBalloonButtonClickedEventArgs:
                    SpawnNewBalloon();
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            for (int i = 0; i < maxRopeController; i++)
            {
                RopeController ropeController = Instantiate(ropeControllerPrefab, transform.position, Quaternion.identity, transform);
                ropeController.gameObject.SetActive(false);
                ropeControllers.Add(ropeController);
                ropeController.holdingHand = holdingHand;
                ropeController.balloon.target = target;
                ropeController.balloon.balloonMat.color = RandomColorGenerator.GetRandomColor();
                _ropeControllersQueue.Enqueue(ropeController);
            }
        }

        private void SpawnNewBalloon()
        {
            if (_ropeControllersQueue.Count > 0)
            {
                RopeController ropeController = _ropeControllersQueue.Dequeue();
                ropeController.gameObject.SetActive(true);

                Vector3 randomPos = RandomPositionFromVector.FindClosePosition(holdingHand.position, 4);
                if (randomPos.y <= -4)
                {
                    randomPos.y = -3;
                }
                ropeController.balloon.transform.position = new Vector3(randomPos.x, randomPos.y, randomPos.z);
            }
            
        }
    }
}