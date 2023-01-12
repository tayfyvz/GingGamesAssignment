using _GameFiles.Scripts.Controllers.BalloonTower;
using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers.BalloonTower
{
    public class BalloonTowerLevelManager : BaseManager
    {
        [SerializeField] private RopeController ropeController;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case AddBalloonButtonClickedEventArgs addBalloonButtonClickedEventArgs:
                    //Add new Balloon
                    break;
            }
        }
    }
}