using System.Collections.Generic;
using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public struct VertexTriangles
    {
        public List<Vector3> Vertices;
        public List<int> Triangles;
        public VertexTriangles(Vector3 temp)
        {
            Vertices = new List<Vector3>(new Vector3[8]);
            
            for (int i = 0; i < Vertices.Count; i++)
            {
                Vertices[i] = temp;
            }

            Triangles = new List<int>();
            int[] values = {0, 2, 1, 0, 3, 2, 2, 3, 4, 2, 4, 5, 1, 2, 5, 1, 5, 6, 0, 7, 4, 0, 4, 3, 5, 4, 7, 5, 7, 6, 0, 6, 7, 0, 1, 6};
            for (int i = 0; i < 36; i++)
            {
                int value = values[i];
                Triangles.Add(value);
            }
        }

        public void RangedVertexTriangles(Vector3 lastMousePos, Camera cam)
        {
            Vertices.AddRange(new Vector3[4]);
            Triangles.AddRange(new int[30]);

            int vIndex = Vertices.Count - 8;
            int vIndex0 = vIndex + 3;
            int vIndex1 = vIndex + 2;
            int vIndex2 = vIndex + 1;
            int vIndex3 = vIndex + 0;
            int vIndex4 = vIndex + 4;
            int vIndex5 = vIndex + 5;
            int vIndex6 = vIndex + 6;
            int vIndex7 = vIndex + 7;

            Vector3 currentMousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            Vector3 mouseForward = (currentMousePos - lastMousePos).normalized;

            float lineThickness = .25f;
                
            Vector3 topRightVertex = currentMousePos + Vector3.Cross(mouseForward, Vector3.back) * lineThickness;
            Vector3 bottomRightVertex = currentMousePos + Vector3.Cross(mouseForward, Vector3.forward) * lineThickness;
            Vector3 topLeftVertex = new Vector3(topRightVertex.x, topRightVertex.y, 1);
            Vector3 bottomLeftVertex = new Vector3(bottomRightVertex.x, bottomRightVertex.y, 1);

            Vertices[vIndex4] = topLeftVertex;
            Vertices[vIndex5] = topRightVertex;
            Vertices[vIndex6] = bottomRightVertex;
            Vertices[vIndex7] = bottomLeftVertex;

            int tIndex = Triangles.Count - 30;
            int[] value =
            {
                vIndex2, vIndex3, vIndex4, vIndex2, vIndex4, vIndex5, vIndex1, vIndex2, vIndex5, vIndex1, vIndex5,
                vIndex6, vIndex0, vIndex7, vIndex4, vIndex0, vIndex4, vIndex3, vIndex5, vIndex4, vIndex7, vIndex0,
                vIndex4, vIndex3, vIndex0, vIndex6, vIndex7, vIndex0, vIndex1, vIndex6
            };
            for (int i = 0; i < 30; i++)
            {
                Triangles[tIndex + i] = value[i];
            }
        }
        
    }
}