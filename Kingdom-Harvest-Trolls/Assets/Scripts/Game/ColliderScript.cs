using Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ColliderScript : MonoBehaviour
{
    public int index_i;
    public int index_j;

    GameController gameController;
    FieldScript fieldScript;
    CellsScript cellsScript;
    ColliderPanelScript colliders;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        fieldScript = GameObject.Find("FieldPanel").GetComponent<FieldScript>();
        cellsScript = GameObject.Find("FieldPanel").GetComponent<CellsScript>();
        colliders = GameObject.Find("CheckPanel").GetComponent<ColliderPanelScript>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if ((fieldScript.cells[index_i, index_j].destroyable) && (!fieldScript.cells[index_i, index_j].is_destroyed))
        {
            if (collision.gameObject.tag == "Troll")
            {

                Cell cell = fieldScript.cells[index_i, index_j];

                Cell new_cell = fieldScript.FindCellByType(cell.type, -10, cell.count_of_road, true);

                gameController.UpgrateCellInfo(index_i, index_j, new_cell);

                colliders.ChangeCellTag(index_i, index_j, "UI");
            }
        }
    }
}
