using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColliderPanelScript : MonoBehaviour
{
    public GameObject GameController;
    private GameController gameController;

    FieldScript fieldScript;
    CellsScript cellsScript;
    public GameObject Field;

    public GameObject checkPanel;

    private int width;
    private int height;

    public GameObject collider;

    public GameObject[,] checks;

    Vector2[,] castles;

    System.Random random = new System.Random();

    private void Start()
    {
        gameController = GameController.GetComponent<GameController>();
        fieldScript = Field.GetComponent<FieldScript>();

        width = fieldScript.width;
        height = fieldScript.height;

        cellsScript = Field.GetComponent<CellsScript>();

        Vector2 cellSize = Field.GetComponent<GridLayoutGroup>().cellSize;
        checkPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize.x * width, cellSize.y * height);

        InitColliders();
    }

    public void InitColliders()
    {
        checks = new GameObject[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                InitCollider(i, j);
                checks[i, j].gameObject.GetComponent<ColliderScript>().index_i = i;
                checks[i, j].gameObject.GetComponent<ColliderScript>().index_j = j;
            }
    }

    private void InitCollider(int i, int j)
    {
        checks[i, j] = Instantiate(collider, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = checks[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        checks[i, j].SetActive(true);
    }

    public void ChangeCellTag(int x, int y, string new_tag)
    {
        if (checks == null)
        {
            InitColliders();
        }

        Debug.Log($"FUCK {width} {height}");

        checks[x, y].gameObject.tag = new_tag;
    }
}
