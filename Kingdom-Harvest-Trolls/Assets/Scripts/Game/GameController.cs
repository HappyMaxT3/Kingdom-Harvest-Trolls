using Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int coin_amount;
    public int wheat_amount;
    public int knight_amount;

    public TextMeshProUGUI coin_amount_text;
    public TextMeshProUGUI wheat_amount_text;
    public TextMeshProUGUI knight_amount_text;

    FieldScript fieldScript;
    CellsScript cellsScript;
    public GameObject Field;

    Sprite sprite = null;

    public GameObject optionPanel;
    public GameObject cellPressedPanel;

    public TextMeshProUGUI timer;

    private int sec_0_60;

    public Image image;
    public Cell new_cell;

    private int x, y;

    private void Start()
    {
        fieldScript = Field.GetComponent<FieldScript>();
        cellsScript = Field.GetComponent<CellsScript>();

        sec_0_60 = 60;
        InvokeRepeating("Timer", 1f, 1f);
    }

    public void NewCellOnClick(GameObject cell)
    {
        sprite = cell.GetComponent<Image>().sprite;
        image.sprite = sprite;

        for (int i = 0; i < cellsScript.all_cells.Length; i++)
        {
            if (cellsScript.all_cells[i].sprite == sprite)
            {
                new_cell = cellsScript.all_cells[i];
                break;
            }
        }
    }

    public void FieldCellOnClick(GameObject cell, int index_i, int index_j)
    {
        x = index_i;
        y = index_j;
        if ((sprite != null) && (fieldScript.cells[index_i, index_j].title == null))
        {
            Debug.Log("CLICK");
            cell.GetComponent<Image>().sprite = sprite;
            cell.GetComponent<Image>().transform.localEulerAngles = new Vector3(0, 0, 90 * new_cell.rotation);

            EditCell();

            fieldScript.dark_cells[index_i, index_j] = cell;
            fieldScript.cells[index_i, index_j] = new_cell;

            sprite = null;
            new_cell.rotation = 0;
            image.sprite = null;
            image.transform.localEulerAngles = new Vector3(0, 0, 0);

            cellsScript.RandomCell();
        }
        else if (sprite == null)
        {
            fieldScript.OnCellClick(index_i, index_j);
            if ((fieldScript.cells[index_i, index_j].coin_per_time > 0) || (fieldScript.cells[index_i, index_j].wheat_per_time > 0))
            {
                OpenCellPressedPanel();
                UpdateClaimPanel();
            }
        }
    }

    public void EditCell()
    {
        while (new_cell.rotation > 0)
        {
            string temp = new_cell.down;
            new_cell.down = new_cell.left;
            new_cell.left = new_cell.up;
            new_cell.up = new_cell.right;
            new_cell.right = temp;

            new_cell.rotation--;
        }
    }

    public void SpriteOnClick()
    {
        new_cell.rotation = (new_cell.rotation + 1) % 4;
        image.transform.localEulerAngles = new Vector3(0, 0, 90 * new_cell.rotation);
    }

    public void IncreaseAmount()
    {
        if (fieldScript.cells[x, y].coin_per_time > 0)
        {
            IncreaseCoinAmount(fieldScript.cells[x, y].coin_amount);
            fieldScript.cells[x, y].coin_amount = 0;
            UpdateClaimPanel();
        }
        if (fieldScript.cells[x, y].wheat_per_time > 0)
        {
            IncreaseWheatAmount(fieldScript.cells[x, y].wheat_amount);
            fieldScript.cells[x, y].wheat_amount = 0;
            UpdateClaimPanel();
        }
    }

    public void UpdateClaimPanel()
    {
        cellPressedPanel.gameObject.GetComponent<CellPressedPanelScript>().ChangeTitle(fieldScript.cells[x, y]);
    }

    public void IncreaseCoinAmount(int amount)
    {
        coin_amount += amount;
        coin_amount_text.text = coin_amount.ToString();
    }

    public void IncreaseWheatAmount(int amount)
    {
        wheat_amount += amount;
        wheat_amount_text.text = wheat_amount.ToString();
    }

    public void OpenOptionPanel()
    {
        optionPanel.gameObject.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        optionPanel.gameObject.SetActive(false);
    }

    public void OpenCellPressedPanel()
    {
        cellPressedPanel.gameObject.SetActive(true);
    }

    public void CloseCellPressedPanel()
    {
        cellPressedPanel.gameObject.SetActive(false);
    }

    private void Timer()
    {
        sec_0_60 = sec_0_60 - 1;
        if (sec_0_60 == -1) sec_0_60 = 60;
        timer.text = sec_0_60.ToString();
    }
}
