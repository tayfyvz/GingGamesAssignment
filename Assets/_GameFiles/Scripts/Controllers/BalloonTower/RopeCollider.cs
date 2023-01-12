using UnityEngine;

namespace _GameFiles.Scripts.Controllers.BalloonTower
{
    public class RopeCollider : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private EdgeCollider2D _edgeCollider2D;
        private Vector3 _ropePoints;
        private Vector2[] _edgeColliderPoints;
        private void Start()
        {
            _edgeCollider2D = this.gameObject.AddComponent<EdgeCollider2D>();
            _lineRenderer = this.gameObject.GetComponent<LineRenderer>();
            _edgeColliderPoints = new Vector2[_lineRenderer.positionCount];
        }
        private void Update()
        {
            GetNewPositions();
            _edgeCollider2D.points = _edgeColliderPoints;
        }
        void GetNewPositions()
        {
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                _ropePoints = _lineRenderer.GetPosition(i);
                _edgeColliderPoints[i] = new Vector2(_ropePoints.x, _ropePoints.y);
            }
        }
    }
}