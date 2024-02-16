using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueChoiceButton : MonoBehaviour
{
    public DialogueUIManager dialogueUIManager;
    public DialogueSO nextDialogue;
    public bool isEnd = false;
    public void Setup(string text, DialogueSO nextDialogueSO)
    {
        isEnd = false;

        nextDialogue = nextDialogueSO;
        if (nextDialogue == null)
        {
            isEnd = true;
            GetComponentInChildren<TextMeshProUGUI>().text = "End";
        }
        else
        {
            GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }

    public void ActivateButton()
    {
        if (isEnd)
        {
            dialogueUIManager.EndDialogue();
            return;
        }
        dialogueUIManager.NextDialogue(nextDialogue);
    }
}
