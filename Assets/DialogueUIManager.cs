using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager instance;
    public CharacterDialogueController dialogueController;
    public DialogueSO currentDialogue;
    public TextMeshProUGUI dialogueText;

    public GameObject dialoguePanel;
    public DialogueChoicePanel dialogueChoicePanel;
    Tween textTween;
    public float textSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M)) 
        //{
        //    StartDialogue();
        //}
    }

    public void StartDialogue(CharacterDialogueController activeDialogue) {
        activeDialogue.dialogue.SelectDialogues();
        dialoguePanel.SetActive(true);
        currentDialogue = activeDialogue.FindStartingDialogue();
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);

        DOTween.Complete(textTween);
        string text = "";
        textTween = DOTween.To(() => text, x=> text = x, currentDialogue.text, textSpeed).SetEase(Ease.Linear).OnUpdate(()=>
        { 
            SetDialogueText(text);            
        });
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
