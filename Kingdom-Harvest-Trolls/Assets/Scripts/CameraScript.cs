using UnityEngine;

namespace Game
{
    public class MapDrag : MonoBehaviour
    {
        public Camera mainCamera;  // ������, ����� ������� ������� �� �����
        private Vector3 dragOrigin;  // ����� ������ ��������������
        private bool isDragging = false;  // ���� ��������������

        void Update()
        {
            // ������ �������������� ��� ������� ����� ������ ����
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
            }

            // ���������� �������������� ��� ���������� ����� ������ ����
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            // �������������� �����
            if (isDragging)
            {
                Vector3 currentMousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                Vector3 difference = dragOrigin - currentMousePos;

                mainCamera.transform.position += difference;
            }
        }
    }
}


