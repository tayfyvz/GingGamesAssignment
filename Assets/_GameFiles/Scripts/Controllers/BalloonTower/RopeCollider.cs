using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public class RopeCollider : MonoBehaviour
    {
        LineRenderer _rope;
        EdgeCollider2D _edgeCollider;
        Vector3 _points;
        Vector2[] _points2;
        private void Start()
        {
            _edgeCollider = this.gameObject.AddComponent<EdgeCollider2D>();
            _rope = this.gameObject.GetComponent<LineRenderer>();
            _points2 = new Vector2[_rope.positionCount];
        }
        private void Update()
        {
            GetNewPositions();
            _edgeCollider.points = _points2;
        }
        void GetNewPositions()
        {
            for (int i = 0; i < _rope.positionCount; i++)
            {
                _points = _rope.GetPosition(i);
                _points2[i] = new Vector2(_points.x, _points.y);
            }
        }
    }
}