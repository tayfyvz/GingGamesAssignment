using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class CreateGameObject
    {
        public static GameObject Create(string name)
        {
            GameObject go = new GameObject(name);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.AddComponent<MeshFilter>();
            go.AddComponent<MeshRenderer>();
            return go;
        }
    }
}