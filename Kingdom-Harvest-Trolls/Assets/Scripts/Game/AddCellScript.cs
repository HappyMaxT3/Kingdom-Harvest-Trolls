using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddCellScript : MonoBehaviour, IPointerClickHandler
{
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        gameController.FieldCellOnClick(transform.gameObject);
    }
}
