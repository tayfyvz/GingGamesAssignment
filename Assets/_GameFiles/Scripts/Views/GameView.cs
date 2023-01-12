using TadPoleFramework.UI;
using UnityEngine;
using UnityEngine.UI;

namespace TadPoleFramework
{
    public class GameView : BaseView
    {
        [Header("Balloon Tower")]
        [SerializeField] private Button balloonTowerButton;
        [SerializeField] private Button addBalloonButton;

        [Header("Draw Chute")]
        [SerializeField] private Button drawChuteButton;

        protected override void Initialize()
        {
            balloonTowerButton.onClick.AddListener((_presenter as GameViewPresenter).OnBalloonTowerButtonClicked);
            addBalloonButton.onClick.AddListener((_presenter as GameViewPresenter).OnAddBalloonButtonClicked);
            
            drawChuteButton.onClick.AddListener((_presenter as GameViewPresenter).OnDrawChuteButtonClicked);
        }

        public void EnableBalloonTowerButton()
        {
            balloonTowerButton.gameObject.SetActive(true);
            drawChuteButton.gameObject.SetActive(false);
            addBalloonButton.gameObject.SetActive(false);
        }

        public void EnableDrawChuteButton()
        {
            drawChuteButton.gameObject.SetActive(true);
            addBalloonButton.gameObject.SetActive(true);
            balloonTowerButton.gameObject.SetActive(false);
        }
    }
}