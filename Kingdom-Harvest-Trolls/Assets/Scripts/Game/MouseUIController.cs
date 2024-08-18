using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Game
{
    public class MouseUIController : MonoBehaviour
    {
        public float cameraSpeed;

        public float minZoom;
        public float maxZoom;

        float zoom;

        public GameObject Panel;

        private Vector3 startPosition;

        private Vector3 cursorPosition;

        private void Start()
        {
            startPosition = Panel.transform.position;
        }

        private void Update()
        {
            Zoom();
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<CanvasScaler>().scaleFactor = zoom;

            startPosition = Input.mousePosition;
        }

        void Zoom()
        {
            if (Input.GetKeyUp(KeyCode.KeypadMinus))
            {
                zoom -= cameraSpeed * Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                zoom += cameraSpeed * Time.deltaTime;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                zoom -= cameraSpeed * Time.deltaTime * 10f;
            }
            if (Input.mouseScrollDelta.y > 0)
            {
                zoom += cameraSpeed * Time.deltaTime * 10f;
            }
        }

        public void Move()
        {
            Panel.transform.position = new Vector3(
                Input.mousePosition.x - startPosition.x + Panel.transform.position.x,
                Input.mousePosition.y - startPosition.y + Panel.transform.position.y,
                0
            );
            startPosition = Input.mousePosition;
        }
    }
}

