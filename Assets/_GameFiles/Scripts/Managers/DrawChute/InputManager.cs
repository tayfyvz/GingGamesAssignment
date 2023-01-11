using TadPoleFramework;
using TadPoleFramework.Core;
using UnityEngine;

namespace _GameFiles.Scripts.Managers.DrawChute
{
    public class InputManager : BaseManager
    {
        public override void Receive(BaseEventArgs baseEventArgs)
        {
            
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                BroadcastUpward(new StartDrawEventArgs());
            }
            else if (Input.GetMouseButtonUp(0))
            {
                BroadcastUpward(new EndDrawEventArgs());
            }
        }
    }
}