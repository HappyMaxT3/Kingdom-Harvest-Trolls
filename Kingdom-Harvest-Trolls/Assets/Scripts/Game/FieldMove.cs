using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FieldMove : MonoBehaviour
{
    private Vector2 startPosition;
    public GameObject Panel;

    private Vector3 previousMousePosition = new Vector3(0, 0, 0);

    private void Start()
    {
        startPosition = Panel.transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           MovePanel();
        }
    }

    private void MovePanel()
    {
        float position_x = Input.mousePosition.x - previousMousePosition.x - startPosition.x;
        float position_y = Input.mousePosition.y - previousMousePosition.y - startPosition.y;

        Panel.transform.position = new Vector3(position_x, position_y, 0);
    }
}
