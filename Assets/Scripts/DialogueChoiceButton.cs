using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

public class DialogueChoiceButton : MonoBehaviour
{
    public DialogueUIManager dialogueUIManager;
    public DialogueSO nextDialogue;
    public DialogueSO currentDialogue;
    public bool isEnd = false;
    public DialogueActions action;
    public bool hasQuestHandin = false;
    public QuestSO pickupQuest;
    public QuestSO handinQuest;
    public DialogueContainerSO dialogueAfterCompletion;
    public TextMeshProUGUI textObject;
    Tween textTween;
    public float textSpeed;
    public AudioSource audioSource;
    public QuestDatabase questDatabase;

    public void Setup(string text, DialogueSO nextDialogueSO, DialogueActions _action, QuestSO _pickupQuest, QuestSO _handinQuest, DialogueContainerSO _dialogueAfterCompletion)
    {
        isEnd = false;
        nextDialogue = nextDialogueSO;
        action = _action;
        pickupQuest = _pickupQuest;
        handinQuest = _handinQuest;
        dialogueAfterCompletion = _dialogueAfterCompletion;

        if (nextDialogue == null)
        {
            isEnd = true;
            //GetComponentInChildren<TextMeshProUGUI>().text = "End";            
        }
        SetDialogueText(text);
        //DOTween.Complete(textTween);
        //string newText = "";
        //textTween = DOTween.To(() => newText, x => newText = x, text, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        //{
            //SetDialogueText(newText);
        //}); 
        //GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (pickupQuest != null)
        {
            if (!questDatabase.GetQuestStatus(pickupQuest.questID, QuestStatus.NotStarted))
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
        }

        //hand in quest is available
        if (handinQuest != null)
        {
            if (!questDatabase.GetQuestStatus(handinQuest.questID, QuestStatus.Completed))
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
        }
    }

    private void SetDialogueText(string newText)
    {
        textObject.text = newText;
    }

    public void ActivateButton()
    {
        DialogueFunctionManager.instance.Activate(action, pickupQuest, handinQuest);
        if (dialogueAfterCompletion != null)
        {
            dialogueUIManager.dialogueController.dialogue.dialogueContainer = dialogueAfterCompletion;
        }

        if (isEnd)
        {
            if (action != DialogueActions.OpenShop)
            {
                PlayerManager.instance.ExitShop();
                PlayerManager.instance.currentVendor.ExitShop();
            }

            dialogueUIManager.EndDialogue();

            return;
        }
        else
        {
            dialogueUIManager.NextDialogue(nextDialogue);
        }        
    }

    public void Hover()
    {
        DOTween.Complete(textTween);
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOAnchorPosX(-30, .3f);
        audioSource.Play();
    }

    public void Leave()
    {
        DOTween.Complete(textTween);
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOAnchorPosX(0, .3f);
    }
}
