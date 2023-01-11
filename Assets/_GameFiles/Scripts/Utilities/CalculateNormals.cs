using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.MeshOperations;

namespace _GameFiles.Scripts.Utilities
{
    public static class CalculateNormals
    {
        public static void Calculate(GameObject go)
        {
            new MeshImporter(go).Import();

            ProBuilderMesh proMesh = go.GetComponent<ProBuilderMesh>();
            
            Normals.CalculateNormals(proMesh);
            
            proMesh.ToMesh();
            proMesh.Refresh();
        }
    }
}