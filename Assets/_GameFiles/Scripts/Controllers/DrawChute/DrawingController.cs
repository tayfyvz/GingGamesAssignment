using System.Collections;
using _GameFiles.Scripts.Utilities;
using UnityEngine;


namespace _GameFiles.Scripts.Controllers.DrawChute
{
    public class DrawingController : MonoBehaviour
    {
        [SerializeField] private MeshCollider drawingArea;
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject chute;

        private GameObject _drawing;
        private Coroutine _drawCoroutine;

        private bool IsDrawArea =>
            drawingArea.bounds.Contains(
                cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 11)));

        private bool _isStarted;
        public void DrawEnabled()
        {
            if (!IsDrawArea)
            {
                return;
            }
            _drawCoroutine = StartCoroutine(Draw());
        }

        public void DrawDisabled()
        {
            if (!_isStarted)
            {
                return;
            }

            _isStarted = false;
            
            if (_drawCoroutine != null)
            {
                StopCoroutine(_drawCoroutine);
            }
            
            CreateDraw();
            CalculateNormals.Calculate(_drawing);
            
            Mesh mesh = _drawing.GetComponent<MeshFilter>().mesh;

            Mesh chuteMesh = new Mesh
            {
                vertices = mesh.vertices,
                triangles = mesh.triangles,
                normals = mesh.normals
            };

            chute.GetComponent<MeshFilter>().mesh = chuteMesh;
            chute.GetComponent<MeshFilter>().sharedMesh = chuteMesh;
            
            Bounds chuteBounds = chute.GetComponent<MeshFilter>().mesh.bounds;
            Vector3 middlePoint = new Vector3(0,0,25);
            Vector3 offset = chute.transform.position - chute.transform.TransformPoint(chuteBounds.center);
            chute.transform.position = middlePoint + offset;
            Destroy(_drawing);
        }

        private IEnumerator Draw()
        {
            _isStarted = true;
            _drawing = CreateGameObject.Create("drawing");
            Vector3 startPos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            AddMeshToGameObject.AddMesh(_drawing, startPos);

            Vector3 lastMousePos = startPos;
            
            while (IsDrawArea)
            {
                float minDistance = .1f;

                float distance =
                    Vector3.Distance(
                        cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                        lastMousePos);
                while (distance < minDistance)
                {
                    distance = Vector3.Distance(
                        cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                        lastMousePos);
                    yield return null;
                }
                lastMousePos = AddMeshToGameObject.AddRangeMesh(lastMousePos, cam);
                yield return null;
            }
        }

        private void CreateDraw()
        {
            Mesh mesh = _drawing.GetComponent<MeshFilter>().mesh;
            Vector3[] vertices = mesh.vertices;

            for (int i = 1; i < vertices.Length; i++)
            {
                vertices[i] = new Vector3(vertices[i].x + (vertices[0].x * -1), vertices[i].y + (vertices[0].y * -1),
                    vertices[i].z + (vertices[0].z * -1));
            }
            vertices[0] = Vector3.zero;
            mesh.vertices = vertices;
        }
    }
}