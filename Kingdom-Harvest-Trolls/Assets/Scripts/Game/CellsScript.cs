using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Cell
{
    public Sprite sprite;
    public string type;
    public bool is_castle;
    public bool is_destroyed;
    public int coin_per_time;
    public int wheat_per_time;
    public int level;
    public int count_of_road;
    public bool is_just_road;

    public Cell(Sprite _sprite, string _type, bool _is_castle, bool _is_destroyed, int _coin_per_time, int _wheat_per_time, int _level, int _count_of_road, bool _is_just_road)
    {
        sprite = _sprite;
        type = _type;
        is_castle = _is_castle;
        is_destroyed = _is_destroyed;
        coin_per_time = _coin_per_time;
        wheat_per_time = _wheat_per_time;
        level = _level;
        count_of_road = _count_of_road;
        is_just_road = _is_just_road;
    }
}

public class CellsScript : MonoBehaviour
{
    public Cell[] all_cells = new Cell[26];
    public Cell[] choose_cells = new Cell[26];

    public Cell[] cellChoose = new Cell[3];

    System.Random random = new System.Random();

    public Image cell1;
    public Image cell2;
    public Image cell3;

    private void Start()
    {
        for (int i = 0; i < all_cells.Length; i++)
        {
            choose_cells[i] = all_cells[i % 3];
            if (!all_cells[i].is_destroyed && all_cells[i].level == 0)
            {
                choose_cells[i] = all_cells[i];
                /*
                    all_cells[i].sprite, 
                    all_cells[i].type, 
                    all_cells[i].is_castle, 
                    all_cells[i].is_destroyed, 
                    all_cells[i].coin_per_time,
                    all_cells[i].wheat_per_time, 
                    all_cells[i].level,
                    all_cells[i].count_of_road,
                    all_cells[i].is_just_road
                */
            }
            //Debug.Log(choose_cells[5].type);

            RandomCell();
        }
    }

    public void RandomCell()
    {
        for (int i = 0; i < cellChoose.Length; i++)
        {
            cellChoose[i] = choose_cells[random.Next(0, choose_cells.Length)];
            //Debug.Log(cellChoose[i].type);
        }

        cell1.sprite = cellChoose[0].sprite;
        cell2.sprite = cellChoose[1].sprite;
        cell3.sprite = cellChoose[2].sprite;
    }
}
