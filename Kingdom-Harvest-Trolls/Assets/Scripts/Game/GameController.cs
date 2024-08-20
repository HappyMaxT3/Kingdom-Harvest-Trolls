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
    public Sprite empty_sprite;

    public GameObject optionPanel;
    public GameObject cellPressedPanel;

    public GameObject OkayPanel;
    public GameObject CastleOkayPanel;
    public GameObject NotOkayPanel;
    public GameObject UpgrateCastlePanel;
    public GameObject BuyKnightsPanel;

    public GameObject Dude;
    public TextMeshProUGUI dudesText;

    public TextMeshProUGUI timer1;
    public TextMeshProUGUI timer2;

    private int sec_0_60;

    public Image image;
    public Cell new_cell;

    private int x, y;

    private string not_enougth = "My lord, you do not have enougth of materials to do the action.";

    private void Start()
    {
        fieldScript = Field.GetComponent<FieldScript>();
        cellsScript = Field.GetComponent<CellsScript>();

        CloseBuyKnightsPanel();
        CloseCastleOkayPanel();
        CloseCellPressedPanel();
        CloseNotOkayPanel();
        CloseOkayPanel();
        CloseOptionPanel();
        CloseUpgrateCastlePanel();
        CloseDude();

        IncreaseCoinAmount(0);
        IncreaseWheatAmount(0);

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
            image.sprite = empty_sprite;
            image.transform.localEulerAngles = new Vector3(0, 0, 0);

            cellsScript.RandomCell();
        }
        else if (sprite == null)
        {
            fieldScript.OnCellClick(index_i, index_j);
            if ((fieldScript.cells[index_i, index_j].coin_per_time > 0) || (fieldScript.cells[index_i, index_j].wheat_per_time > 0))
            {
                OpenCellPressedPanel(index_i, index_j);
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

        if (coin_amount < 0)
        {
            coin_amount = 0;
            OpenDude(not_enougth);
        }
        else
        {
            coin_amount_text.text = coin_amount.ToString();
        }
    }

    public void IncreaseWheatAmount(int amount)
    {
        wheat_amount += amount;
        wheat_amount_text.text = wheat_amount.ToString();

        Cell new_wheat = fieldScript.FindCellByType("wheat", 0, 0, false);
        UpgrateCellInfo(new_wheat);
    }

    public void OpenCellPressedPanel(int index_i, int index_j)
    {
        cellPressedPanel.gameObject.SetActive(true);

        if (fieldScript.cells[index_i, index_j].is_destroyed == true)
        {
            OpenNotOkayPanel();
        }
        else
        {
            if (fieldScript.cells[index_i, index_j].type == "castle")
            {
                OpenCastleOkayPanel();
            }
            else
            {
                OpenOkayPanel();
            }
        }
    }

    public void OpenOptionPanel()
    {
        optionPanel.gameObject.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        optionPanel.gameObject.SetActive(false);
    }

    public void CloseCellPressedPanel()
    {
        CloseBuyKnightsPanel();
        CloseCastleOkayPanel();
        CloseNotOkayPanel();
        CloseOkayPanel();
        CloseUpgrateCastlePanel();

        cellPressedPanel.gameObject.SetActive(false);
    }

    public void OpenOkayPanel()
    {
        OkayPanel.gameObject.SetActive(true);
    }

    public void CloseOkayPanel()
    {
        OkayPanel.gameObject.SetActive(false);
    }

    public void OpenCastleOkayPanel()
    {
        CastleOkayPanel.gameObject.SetActive(true);
    }

    public void CloseCastleOkayPanel()
    {
        CastleOkayPanel.gameObject.SetActive(false);
    }

    public void OpenNotOkayPanel()
    {
        NotOkayPanel.gameObject.SetActive(true);
    }

    public void CloseNotOkayPanel()
    {
        NotOkayPanel.gameObject.SetActive(false);
    }

    public void OpenBuyKnightsPanel()
    {
        BuyKnightsPanel.gameObject.SetActive(true);
    }

    public void CloseBuyKnightsPanel()
    {
        BuyKnightsPanel.gameObject.SetActive(false);
    }

    public void OpenUpgrateCastlePanel()
    {
        UpgrateCastlePanel.gameObject.SetActive(true);

        UpdateUpgratePanelInfo();
    }

    public void UpgrateCastle()
    {
        int next_lvl = fieldScript.cells[x, y].level + 1;
        Cell new_castle = fieldScript.FindCellByType("castle", next_lvl, 1, false);

        if (coin_amount - new_castle.cost_of_upgrate < 0)
        {
            OpenDude(not_enougth);
        }
        else
        {
            IncreaseCoinAmount(-new_castle.cost_of_upgrate);
            UpgrateCellInfo(new_castle);

            UpdateUpgratePanelInfo();
        }
    }

    public void UpdateUpgratePanelInfo()
    {   
        UpgrateCastlePanelScript script = UpgrateCastlePanel.GetComponent<UpgrateCastlePanelScript>();

        int castle_current_lvl = fieldScript.cells[x, y].level + 1;
        int current_coin_per_time = fieldScript.cells[x, y].coin_per_time;
        Cell new_castle = fieldScript.FindCellByType("castle", castle_current_lvl, 1, false);
        int cost = new_castle.cost_of_upgrate;

        if (fieldScript.cells[x, y].level < 2)
        {

            script.level_upgrate.text = castle_current_lvl.ToString() + " -> " + (castle_current_lvl + 1).ToString();

            int next_coin_per_time = fieldScript.FindCellByType("castle", castle_current_lvl, 1, false).coin_per_time;
            script.coin_upgrate.text = current_coin_per_time.ToString() + " -> " + next_coin_per_time.ToString();

            //KNIGHTS

            script.cost.text = cost.ToString();
        } 
        else
        {
            script.level_upgrate.text = castle_current_lvl.ToString();

            script.coin_upgrate.text = current_coin_per_time.ToString();

            //KNIGHTS

            script.cost.text = cost.ToString();

            script.upgrate_button.gameObject.SetActive(false);
        }
    }

    public void CloseUpgrateCastlePanel()
    {
        UpgrateCastlePanel.gameObject.SetActive(false);
    }

    public void UpgrateCellInfo(Cell new_cell)
    {
        fieldScript.cells[x, y] = new_cell;
        fieldScript.dark_cells[x, y].GetComponent<Image>().sprite = new_cell.sprite;
    }

    public void OpenDude(string text)
    {
        Dude.gameObject.SetActive(true);
        dudesText.text = text;
    }

    public void CloseDude()
    {
        Dude.gameObject.SetActive(false);
        dudesText.text = "";
    }

    private void Timer()
    {
        sec_0_60 = sec_0_60 - 1;
        if (sec_0_60 == -1) sec_0_60 = 60;
        timer1.text = sec_0_60.ToString();
        timer2.text = sec_0_60.ToString();
    }
}
