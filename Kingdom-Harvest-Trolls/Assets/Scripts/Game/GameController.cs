using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Sprite sprite = null;

    public GameObject optionPanel;

    public Image image;

    public void NewCellOnClick(GameObject cell)
    {
        //Debug.Log(sprite);
        sprite = cell.GetComponent<Image>().sprite;
        image.sprite = sprite;
        //Debug.Log(sprite);
    }

    public void FieldCellOnClick(GameObject cell)
    {
        if (sprite != null)
        {
            Debug.Log("CLICK");
            cell.GetComponent<Image>().sprite = sprite;
            sprite = null;
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
}
