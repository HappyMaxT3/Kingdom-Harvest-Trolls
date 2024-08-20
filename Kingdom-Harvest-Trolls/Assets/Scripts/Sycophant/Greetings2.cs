using TMPro;
using UnityEngine;
using System.Collections;

public class UIControllerTMP11 : MonoBehaviour
{
    public GameObject uiElement;
    public GameObject uiPanel;
    public TextMeshProUGUI textComponent;
    public float displayDuration;
    public float displayDelay;

    public string displayText = "";

    void Start()
    {
        uiPanel.SetActive(false);
        StartCoroutine(ShowAndHideUI());
    }

    private IEnumerator ShowAndHideUI()
    {

        yield return new WaitForSeconds(displayDelay);
        if (uiElement != null)
        {
            uiElement.SetActive(true);
            uiPanel.SetActive(true);
        }

        if (textComponent != null)
        {
            textComponent.text = displayText;
            textComponent.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(displayDuration);

        if (uiElement != null)
        {
            uiElement.SetActive(false);
            uiPanel.SetActive(false);
        }

        if (textComponent != null)
        {
            textComponent.gameObject.SetActive(false);
        }
    }
}
