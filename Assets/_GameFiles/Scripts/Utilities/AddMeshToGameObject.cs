using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class AddMeshToGameObject
    {
        private static Mesh Mesh { get; set; }
        private static VertexTriangles _vertexTriangles;
        public static void AddMesh(GameObject go, Vector3 startPos)
        {
            Mesh = new Mesh();
            Vector3 startPosition = startPos;
            Vector3 temp = new Vector3(startPosition.x, startPosition.y, .5f);
            
            _vertexTriangles = new VertexTriangles(temp);
            Mesh.vertices = _vertexTriangles.Vertices.ToArray();
            Mesh.triangles = _vertexTriangles.Triangles.ToArray();
            
            Material material = go.GetComponent<Renderer>().material;
            go.GetComponent<MeshFilter>().mesh = Mesh;
            material.shader = Shader.Find("Universal Render Pipeline/Unlit");
            material.color = new Color(.1f,.1f,.1f);
        }

        public static Vector3 AddRangeMesh(Vector3 lastMousePos, Camera cam)
        {
            _vertexTriangles.RangedVertexTriangles(lastMousePos, cam);
            Mesh.vertices = _vertexTriangles.Vertices.ToArray();
            Mesh.triangles = _vertexTriangles.Triangles.ToArray();

            lastMousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            
            return lastMousePos;
        }
    }
}