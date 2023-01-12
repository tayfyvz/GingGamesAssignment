using System.Collections;
using TadPoleFramework.Core;
using TadPoleFramework.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TadPoleFramework
{
    public class GameViewPresenter : BasePresenter
    {
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case DrawChuteLevelUIEventArgs drawChuteLevelUIEventArgs:
                    (view as GameView).EnableBalloonTowerButton();
                    break;
                case BalloonTowerLevelUIEventArgs balloonTowerLevelUIEventArgs:
                    (view as GameView).EnableDrawChuteButton();
                    break;
            }
        }

        public void OnBalloonTowerButtonClicked()
        {
            BroadcastUpward(new BalloonTowerButtonClickedEventArgs());
        }

        public void OnDrawChuteButtonClicked()
        {
            BroadcastUpward(new DrawChuteButtonClickedEventArgs());
        }

        public void OnAddBalloonButtonClicked()
        {
            BroadcastUpward(new AddBalloonButtonClickedEventArgs());
        }
    }
}