using UnityEngine;

namespace _GameFiles.Scripts.Utilities
{
    public static class RandomPositionFromVector
    {
        public static Vector3 FindClosePosition(Vector3 position, float radius)
        {
            Vector3 random = position + Random.onUnitSphere * radius;
            return random;
        }
    }
}
