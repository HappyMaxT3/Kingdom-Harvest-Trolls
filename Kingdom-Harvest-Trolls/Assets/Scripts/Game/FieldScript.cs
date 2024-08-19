using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class FieldScript : MonoBehaviour
{
    const int INF = 1000;

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [SerializeField] GameObject panel;

    [SerializeField] GameObject dark_cell_prefab;

    //int[,] cells;
    //GameObject[,] new_cells;

    GameObject[,] dark_cells;
    //bool[,] is_opened;

    private void Start()
    {
        Vector2 cellSize = panel.GetComponent<GridLayoutGroup>().cellSize;
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize.x * width, cellSize.y * height);

        StartNewGame();
    }

    private void Update()
    {
        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                
            }
    }

    public void StartNewGame()
    {
        dark_cells = new GameObject[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
            {
                InitDarkCell(i, j);
            }
    }

    private void InitDarkCell(int i, int j)
    {
        dark_cells[i, j] = Instantiate(dark_cell_prefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = dark_cells[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        dark_cells[i, j].SetActive(true);
    }
}
