using System.ComponentModel;
using _GameFiles.Scripts.Models;
using TadPoleFramework;
using TadPoleFramework.Core;
using TadPoleFramework.UI;
using UnityEngine;


public class UIManager : BaseUIManager
{
    [SerializeField] private GameViewPresenter gameViewPresenter;
    
    private GameModel _gameModel;
    
    public override void Receive(BaseEventArgs baseEventArgs)
    {
        switch (baseEventArgs)
        {
            case SceneStartedEventArgs sceneStartedEventArgs:
                if (_gameModel.Level == 1)
                {
                    BroadcastDownward(new DrawChuteLevelUIEventArgs());
                }
                else
                {
                    BroadcastDownward(new BalloonTowerLevelUIEventArgs());
                }
                break;
            case BalloonTowerButtonClickedEventArgs balloonTowerButtonClickedEventArgs:
                _gameModel.Level = 2;
                break;
            case DrawChuteButtonClickedEventArgs drawChuteButtonClickedEventArgs:
                _gameModel.Level = 1;
                break;
            case AddBalloonButtonClickedEventArgs addBalloonButtonClickedEventArgs:
                Broadcast(addBalloonButtonClickedEventArgs);
                break;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        gameViewPresenter.InjectManager(this);
    }

    protected override void Start()
    {
        base.Start();
        gameViewPresenter.ShowView();
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