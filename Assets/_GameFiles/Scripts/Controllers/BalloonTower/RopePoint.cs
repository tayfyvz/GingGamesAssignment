using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public struct RopePoint
    {
        public Vector3 PosCurrent;
        public Vector3 PosOld;

        public RopePoint(Vector3 pos)
        {
            PosCurrent = pos;
            PosOld = pos;
        }
    }
}