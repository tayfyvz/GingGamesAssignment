using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class AddMeshToGameObject
    {
        public static Mesh Mesh { get; set; }
        public static VertexTriangles VertexTriangles;
        public static void AddMesh(GameObject go, Vector3 startPos)
        {
            Mesh = new Mesh();
            Vector3 startPosition = startPos;
            Vector3 temp = new Vector3(startPosition.x, startPosition.y, .5f);
            
            VertexTriangles = new VertexTriangles(temp);
            Mesh.vertices = VertexTriangles.Vertices.ToArray();
            Mesh.triangles = VertexTriangles.Triangles.ToArray();
            
            go.GetComponent<MeshFilter>().mesh = Mesh;
            go.GetComponent<Renderer>().material.color = Color.green;
        }

        public static Vector3 AddRangeMesh(Vector3 lastMousePos)
        {
            VertexTriangles.RangedVertexTriangles(lastMousePos);
            Mesh.vertices = VertexTriangles.Vertices.ToArray();
            Mesh.triangles = VertexTriangles.Triangles.ToArray();

            lastMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            
            return lastMousePos;
        }
    }
}