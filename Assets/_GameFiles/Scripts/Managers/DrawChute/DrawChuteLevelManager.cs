using _GameFiles.Scripts.Controllers.DrawChute;
using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers.DrawChute
{
    public class DrawChuteLevelManager : BaseManager
    {
        [SerializeField] private InputManager inputManager;
        [SerializeField] private DrawingController drawingController;
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            switch (baseEventArgs)
            {
                case SceneStartedEventArgs sceneStartedEventArgs:
                    
                    break;
                case StartDrawEventArgs startDrawEventArgs:
                    drawingController.DrawEnabled();
                    break;
                case EndDrawEventArgs endDrawEventArgs:
                    drawingController.DrawDisabled();
                    break;
            }
        }

        protected override void Awake()
        {
            base.Awake();
            
            inputManager.InjectManager(this);
        }
    }
}