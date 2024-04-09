using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

//This script handles functionality for the dialog choice buttons
public class DialogueChoiceButton : MonoBehaviour
{
    #region Parameters
    [Header("Dialogue")]
    [SerializeField] DialogueUIManager dialogueUIManager;
    DialogueSO nextDialogue;
    DialogueSO currentDialogue;
    DialogueContainerSO dialogueAfterCompletion;
    [SerializeField] DialogueActions action;


    [Header("Quest Info")]
    //public bool hasQuestHandin = false;
    [SerializeField] QuestSO pickupQuest;
    [SerializeField] QuestSO completeQuest;
    [SerializeField] QuestDatabase questDatabase;

    [Header("Text")]
    Tween textTween;
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] float textSpeed;
    [SerializeField] AudioSource audioSource;

    [Header("Misc")]
    [SerializeField] Button button;
    [SerializeField] RectTransform rect;
    bool isEnd = false;
    #endregion

    #region Button Setup
    public void Clear() {
        textObject.text = "";
    }

    //sets the button functionality up
    public void Setup(string text, DialogueSO nextDialogueSO, DialogueActions _action, QuestSO _pickupQuest, QuestSO _completeQuest, DialogueContainerSO _dialogueAfterCompletion)
    {
        rect.DOAnchorPosX(0, .1f);
        button.interactable = true;
        isEnd = false;
        nextDialogue = nextDialogueSO;
        action = _action;
        pickupQuest = _pickupQuest;
        completeQuest = _completeQuest;
        dialogueAfterCompletion = _dialogueAfterCompletion;

        if (nextDialogue == null)
        {
            isEnd = true;
        }

        SetDialogueText(text);

        if (pickupQuest != null)
        {
            if (!questDatabase.GetQuestStatus(pickupQuest.questID, QuestStatus.NotStarted))
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }
        }

        if (completeQuest != null)
        {
            if (!questDatabase.GetQuestStatus(completeQuest.questID, QuestStatus.Accepted))
            {
                transform.parent.gameObject.SetActive(false);
                return;
            }

            if (completeQuest.itemRequirement.Length > 0)
            {
                if (!DialogueFunctionManager.instance.CanHandIn(completeQuest))
                {
                    transform.parent.gameObject.SetActive(false);
                    Debug.Log("Item not found in inventory");
                    return;
                }
            }
        }
    }

    //updates the text field
    void SetDialogueText(string newText)
    {
        textObject.text = newText;
    }
    #endregion

    #region Button Interactions
    //perform actions based on the DialogueActions applied to the button
    public void ActivateButton()
    {
        DialogueFunctionManager.instance.Activate(action, pickupQuest,completeQuest);
        if (dialogueAfterCompletion != null)
        {
            dialogueUIManager.dialogueController.dialogue.dialogueContainer = dialogueAfterCompletion;
        }

        if (isEnd)
        {
            if (action != DialogueActions.OpenShop)
            {
                PlayerManager.instance.ExitShop();
                PlayerManager.instance.currentVendor.DeactivateShopCam();
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
        if (button.interactable)
        {
            DOTween.Complete(textTween);
            rect.DOAnchorPosX(-30, .3f);
            audioSource.Play();            
        }
    }

    public void Leave()
    {
        if (button.interactable)
        {
            DOTween.Complete(textTween);
            rect.DOAnchorPosX(0, .3f);
        }
    }
    #endregion
}
