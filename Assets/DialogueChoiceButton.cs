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
            if (!PlayerQuestManager.instance.CheckQuestAvailable(pickupQuest))
            {
                if (PlayerQuestManager.instance.CheckIfQuestCompleted(pickupQuest))
                {
                    transform.parent.gameObject.SetActive(false);
                    return;
                }
                
                if (PlayerQuestManager.instance.CheckIfQuestHandedIn(pickupQuest))
                {
                    transform.parent.gameObject.SetActive(false);
                    return;
                }
                return;
            }
        }

        if (handinQuest != null)
        {
            if (!PlayerQuestManager.instance.CheckQuestAvailable(handinQuest))
            {
                if (!PlayerQuestManager.instance.CheckIfQuestCompleted(handinQuest))
                {
                    transform.parent.gameObject.SetActive(false);
                }
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
        //switch (action)
        //{
        //    case DialogueActions.None:
        //        break;
        //    case DialogueActions.OpenShop:
        //        DialogueFunctionManager.instance.HandInQuest(handinQuest);
        //        break;
        //    case DialogueActions.AcceptQuest:
        //        DialogueFunctionManager.instance.AcceptQuest(pickupQuest);
        //        break;
        //    case DialogueActions.HandInQuest:
        //        DialogueFunctionManager.instance.HandInQuest(handinQuest);
        //        break;
        //    default:
        //        break;
        //}
        //if (hasFunction)
        //{
        //    if (pickupQuest != null)
        //    {
        //        DialogueFunctionManager.instance.AcceptQuest(pickupQuest);

        //        //DialogueFunctionManager.instance.AcceptQuest(pickupQuest);
        //    }
            
        //    if (handinQuest != null)
        //    {
        //        DialogueFunctionManager.instance.HandInQuest(handinQuest);
        //    }

        //    else
        //    {
        //        //DialogueFunctionManager.instance.Activate(functionName);
        //    }
        //}

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
    }

    public void Leave()
    {
        DOTween.Complete(textTween);
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOAnchorPosX(0, .3f);
    }
}
