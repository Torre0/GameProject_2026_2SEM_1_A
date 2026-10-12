using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class InteractionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI guideText;
    [SerializeField] private GameObject dialiguePanel;
    [SerializeField] private TextMeshProUGUI dialigueText;
    [SerializeField] private float dialigueDuration = 3.0f;

    private Coroutine dialigueRoutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideGuide();
        dialiguePanel.SetActive(false);
    }

    public void ShowGuide(string message)
    {
        guideText.gameObject.SetActive(true);
        guideText.text = message;
    }

    public void HideGuide()
    {
        guideText.gameObject.SetActive(false);
    }

    private IEnumerator DialogueRoutine(string npcName, string message)
    {
        dialiguePanel.SetActive(true);
        dialigueText.text = npcName + "\n" + message;

        yield return new WaitForSeconds(dialigueDuration);

        dialiguePanel.SetActive(false);
        dialigueRoutine = null;
    }

    public void ShowDialogue(string npcName, string message)
    {
        if (dialigueRoutine != null)
        {
            StopCoroutine(dialigueRoutine);
        }

        dialigueRoutine = StartCoroutine(DialogueRoutine(npcName, message));
    }
}
