using System;
using System.Collections;
using _GameFiles.Scripts.Utilities;
using UnityEngine;

namespace _GameFiles.Scripts.Controllers.DrawChute
{
    public class DrawingController : MonoBehaviour
    {
        private Coroutine _drawCoroutine;
        public void DrawEnabled()
        { 
            _drawCoroutine = StartCoroutine(Draw());
        }

        public void DrawDisabled()
        {
            if (_drawCoroutine != null)
            {
                StopCoroutine(_drawCoroutine);
            }
        }

        private IEnumerator Draw()
        {
            GameObject drawing = CreateGameObject.Create("drawing");
            Vector3 startPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
            AddMeshToGameObject.AddMesh(drawing, startPos);

            Vector3 lastMousePos = startPos;
            
            while (true)
            {
                float minDistance = .1f;

                float distance =
                    Vector3.Distance(
                        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                        lastMousePos);
                while (distance < minDistance)
                {
                    distance = Vector3.Distance(
                        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                        lastMousePos);
                    yield return null;
                }
                lastMousePos = AddMeshToGameObject.AddRangeMesh(lastMousePos);
                yield return null;
            }
        }
    }
}