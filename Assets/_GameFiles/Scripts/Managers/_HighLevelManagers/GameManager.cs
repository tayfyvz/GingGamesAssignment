using System.ComponentModel;
using _GameFiles.Scripts.Managers;
using _GameFiles.Scripts.Models;
using TadPoleFramework.Core;
using TadPoleFramework.Game;
using UnityEngine;


namespace TadPoleFramework
{
    public class GameManager : BaseGameManager
    {
        [SerializeField] private LevelManager levelManager;
        private GameModel _gameModel;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case SceneStartedEventArgs sceneStartedEventArgs:
                    Broadcast(sceneStartedEventArgs);
                    break;
                case AddBalloonButtonClickedEventArgs addBalloonButtonClickedEventArgs:
                    BroadcastDownward(addBalloonButtonClickedEventArgs);
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            IMediator mediator = new BaseMediator();
            levelManager.InjectMediator(mediator);
            levelManager.InjectManager(this);
            levelManager.InjectModel(_gameModel);
        }
        
        public void InjectModel(GameModel gameModel)
        {
            this._gameModel = gameModel;
            this._gameModel.PropertyChanged += GameMOdelProperetyChangedHandler;
        }

        private void GameMOdelProperetyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_gameModel.InstantScore))
            {
                
            }
        }
    }
}