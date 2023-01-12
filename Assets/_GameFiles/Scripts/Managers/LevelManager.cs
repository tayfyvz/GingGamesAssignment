using System.ComponentModel;
using _GameFiles.Scripts.Managers.BalloonTower;
using _GameFiles.Scripts.Managers.DrawChute;
using _GameFiles.Scripts.Models;
using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _GameFiles.Scripts.Managers
{
    public class LevelManager : BaseManager
    {
        [SerializeField] private DrawChuteLevelManager drawChuteLevelManager;
        [SerializeField] private BalloonTowerLevelManager balloonTowerLevelManager;
        private GameModel _gameModel;
        
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case AddBalloonButtonClickedEventArgs addBalloonButtonClickedEventArgs:
                    BroadcastDownward(addBalloonButtonClickedEventArgs);
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            IMediator mediator = new BaseMediator();
            
            if (_gameModel.Level == 1)
            {
                drawChuteLevelManager.InjectMediator(mediator);
                drawChuteLevelManager.InjectManager(this);
                drawChuteLevelManager.gameObject.SetActive(true);
                balloonTowerLevelManager.gameObject.SetActive(false);
            }
            else
            { 
                balloonTowerLevelManager.InjectMediator(mediator); 
                balloonTowerLevelManager.InjectManager(this);
                balloonTowerLevelManager.gameObject.SetActive(true);
                drawChuteLevelManager.gameObject.SetActive(false);
            }
        }

        protected override void Start()
        {
            BroadcastDownward(new SceneStartedEventArgs());
            BroadcastUpward(new SceneStartedEventArgs());
        }
        public void InjectModel(GameModel gameModel)
        {
            _gameModel = gameModel;
            _gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }
        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.Level))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }
    }
}