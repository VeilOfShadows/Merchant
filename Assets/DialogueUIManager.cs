using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueUIManager : MonoBehaviour
{
    public CharacterDialogueController dialogueController;
    public DialogueSO currentDialogue;
    public TextMeshProUGUI dialogueText;

    public GameObject dialoguePanel;
    public DialogueChoicePanel dialogueChoicePanel;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) 
        {
            StartDialogue();
        }
    }

    public void StartDialogue() {
        dialoguePanel.SetActive(true);
        currentDialogue = dialogueController.FindStartingDialogue();
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);
        SetDialogueText(currentDialogue.text);
    }

    public void EndDialogue()
    {        
        dialoguePanel.SetActive(false);
    }

    public void NextDialogue(DialogueSO nextDialogue)
    {
        currentDialogue = nextDialogue;
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);
        SetDialogueText(currentDialogue.text);
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}