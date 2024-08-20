using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using Game;

public class FieldScript : MonoBehaviour
{
    public GameObject GameController;
    private GameController gameController;

    private CellsScript cellsScript;

    public float interval = 60f;

    const int INF = 1000;

    [SerializeField] int width = 10;
    [SerializeField] int height = 10;

    [SerializeField] GameObject panel;

    [SerializeField] GameObject dark_cell_prefab;

    //int[,] cells;
    //GameObject[,] new_cells;

    public GameObject[,] dark_cells;
    public Cell[,] cells;
    //bool[,] is_opened;

    Vector2[,] castles;
    bool[,] visited;

    System.Random random = new System.Random();

    private void Start()
    {
        gameController = GameController.GetComponent<GameController>();

        cellsScript = GetComponent<CellsScript>();

        Vector2 cellSize = panel.GetComponent<GridLayoutGroup>().cellSize;
        panel.GetComponent<RectTransform>().sizeDelta = new Vector2(cellSize.x * width, cellSize.y * height);

        StartNewGame();

        InvokeRepeating("IncreaseAmount", interval, interval);
        InvokeRepeating("WheatGrow", interval / 2, interval / 2);

        visited = new bool[height, width];
    }

    public void IncreaseAmount()
    {
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                if (cells[i, j].is_destroyed == false)
                {
                    if (cells[i, j].coin_per_time > 0)
                    {
                        cells[i, j].coin_amount += cells[i, j].coin_per_time;
                    }
                    if (cells[i, j].wheat_per_time > 0)
                    {
                        cells[i, j].wheat_amount += cells[i, j].wheat_per_time;
                    }

                    if (cells[i, j].type == "wheat")
                    {
                        Cell new_wheat = FindCellByType("wheat", 2, 0, false);
                        int wheat_amount = cells[i, j].wheat_amount;
                        cells[i, j] = new_wheat;
                        cells[i, j].wheat_amount = wheat_amount;
                        dark_cells[i, j].GetComponent<Image>().sprite = new_wheat.sprite;
                    }
                }
            }
        gameController.UpdateClaimPanel();
    }

    public void WheatGrow()
    {
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                if (cells[i, j].is_destroyed == false)
                {
                    if ((cells[i, j].type == "wheat") && (cells[i, j].level == 0))
                    {
                        Cell new_wheat = FindCellByType("wheat", 1, 0, false);
                        cells[i, j] = new_wheat;
                        dark_cells[i, j].GetComponent<Image>().sprite = new_wheat.sprite;
                    }
                }
            }
        gameController.UpdateClaimPanel();
    }

    public void StartNewGame()
    {
        dark_cells = new GameObject[height, width];
        cells = new Cell[height, width];

        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                InitDarkCell(i, j);
                dark_cells[i, j].gameObject.GetComponent<AddCellScript>().index_i = i;
                dark_cells[i, j].gameObject.GetComponent<AddCellScript>().index_j = j;
            }

        PlaceRandomCastle();
    }

    public void OnCellClick(int i, int j)
    {
        //if (cells[i, j].type == "castle")
        if (dark_cells[i, j].GetComponent<Image>() != null)
        {
            Debug.Log("YOOO BROO");
        }
    }

    public void PlaceRandomCastle()
    {
        int castle_x = random.Next(3, height - 3);
        int castle_y = random.Next(3, width - 3);

        Cell cell = FindCellByType("castle", 0, 1, false);
        cells[castle_x, castle_y] = cell;
        dark_cells[castle_x, castle_y].GetComponent<Image>().sprite = cell.sprite;

        cell = FindCellByType("quater_village", 0, 0, false);
        cells[castle_x, castle_y + 1] = cell;
        dark_cells[castle_x, castle_y + 1].GetComponent<Image>().sprite = cell.sprite;

        cell = FindCellByType("quater_village", 0, 2, false);
        cells[castle_x - 1, castle_y] = cell;
        dark_cells[castle_x - 1, castle_y].GetComponent<Image>().sprite = cell.sprite;

        cell = FindCellByType("wheat", 0, 0, false);
        cells[castle_x, castle_y - 1] = cell;
        dark_cells[castle_x, castle_y - 1].GetComponent<Image>().sprite = cell.sprite;

        cell = FindCellByType("road", 0, 4, false);
        cells[castle_x + 1, castle_y] = cell;
        dark_cells[castle_x + 1, castle_y].GetComponent<Image>().sprite = cell.sprite;
    }

    private void InitDarkCell(int i, int j)
    {
        dark_cells[i, j] = Instantiate(dark_cell_prefab, Vector3.zero, Quaternion.identity, transform);
        RectTransform rect_transform = dark_cells[i, j].GetComponent<RectTransform>();

        rect_transform.anchorMin = new Vector2(i * 1.0f / width, j * 1.0f / height);
        rect_transform.anchorMax = new Vector2((i + 1) * 1.0f / width, (j + 1) * 1.0f / height);

        rect_transform.offsetMin = Vector2.zero;
        rect_transform.offsetMax = Vector2.zero;

        int index = random.Next(0, 3);
        dark_cells[i, j].GetComponent<Image>().sprite = cellsScript.all_cells[index].sprite;

        dark_cells[i, j].SetActive(true);
        //Debug.Log($"{dark_cells[i, j].GetComponent<Image>().sprite.name} {i} {j}");
    }

    public Cell FindCellByType(string type, int level, int count_of_road, bool is_destroyed)
    {
        for (int i = 0; i < cellsScript.all_cells.Length; i++)
            if ((cellsScript.all_cells[i].type == type) &&
                (cellsScript.all_cells[i].level == level) &&
                (cellsScript.all_cells[i].count_of_road == count_of_road) &&
                (cellsScript.all_cells[i].is_destroyed == is_destroyed))
            {
                return cellsScript.all_cells[i];
            }
        return cellsScript.all_cells[0];
    }

    /*public void UpdateInformation(int x, int y)
    {
        foreach (Vector2 coords in castles)
        {
            CountCastle(x, y);
            UpdateVisited();
        }
    }

    public int CountCastle(int x, int y)
    {
        visited[x, y] = true;

        int amount = cells[x, y].amount;

        if (x + 1 < height)
            if ((cells[x, y].down == "road") || (cells[x, y].down == "village") || (cells[x, y].down == "wheat"))
                if ((cells[x + 1, y].up == "road") || (cells[x + 1, y].up == "village") || (cells[x + 1, y].up == "wheat"))
                    if (visited[x + 1, y] == false)
                    {
                        amount += CountCastle(x + 1, y);
                    }
        if (x - 1 >= 0)
            if ((cells[x, y].up == "road") || (cells[x, y].up == "village") || (cells[x, y].up == "wheat"))
                if ((cells[x - 1, y].down == "road") || (cells[x - 1, y].down == "village") || (cells[x - 1, y].down == "wheat"))
                    if (visited[x - 1, y] == false)
                    {
                        amount += CountCastle(x - 1, y);
                    }
        if (y + 1 < width)
            if ((cells[x, y].down == "road") || (cells[x, y].down == "village") || (cells[x, y].down == "wheat"))
                if ((cells[x, y + 1].up == "road") || (cells[x, y + 1].up == "village") || (cells[x, y + 1].up == "wheat"))
                    if (visited[x, y + 1] == false)
                    {
                        amount += CountCastle(x, y + 1);
                    }
        if (y - 1 >= 0)
            if ((cells[x, y].up == "road") || (cells[x, y].up == "village") || (cells[x, y].up == "wheat"))
                if ((cells[x, y - 1].down == "road") || (cells[x, y - 1].down == "village") || (cells[x, y - 1].down == "wheat"))
                    if (visited[x, y - 1] == false)
                    {
                        amount += CountCastle(x, y - 1);
                    }
        return amount;
    }

    public void UpdateVisited()
    {
        for (int i = 0; i < height; i++)
            for (int j = 0; j < width; j++)
            {
                visited[i, j] = false;
            }
    }*/
}
