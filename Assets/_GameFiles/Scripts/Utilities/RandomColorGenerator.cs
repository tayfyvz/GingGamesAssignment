using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class RandomColorGenerator
    {
        public static Color GetRandomColor()
        {
            Color random = new Color(
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f), 
                Random.Range(0f, 1f)
            );
            return random;
        }
    }
}