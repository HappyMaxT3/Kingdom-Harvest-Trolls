using TMPro;
using UnityEngine;
using System.Collections;

public class UIControllerTMP : MonoBehaviour
{
    public GameObject uiElement;
    public TextMeshProUGUI textComponent;
    public float displayDuration;
    public AudioSource audioSource;
    public AudioClip clip;

    public string displayText = ""; 

    void Start()
    {
        StartCoroutine(ShowAndHideUI());
    }

    private IEnumerator ShowAndHideUI()
    {
        if (uiElement != null)
        {
            uiElement.SetActive(true);
        }

        if (textComponent != null)
        {
            textComponent.text = displayText;
            textComponent.gameObject.SetActive(true); 
        }

        if(audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }

        yield return new WaitForSeconds(displayDuration);

        if (uiElement != null)
        {
            uiElement.SetActive(false);
        }

        if (textComponent != null)
        {
            textComponent.gameObject.SetActive(false);
        }
    }
}
