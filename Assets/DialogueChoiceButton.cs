using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        GetComponentInChildren<TextMeshProUGUI>().text = text;

        if (pickupQuest != null)
        {
            if (!PlayerQuestManager.instance.CheckQuestAvailable(pickupQuest))
            {
                if (PlayerQuestManager.instance.CheckIfQuestCompleted(pickupQuest))
                {
                    gameObject.SetActive(false);
                    return;
                }
                
                if (PlayerQuestManager.instance.CheckIfQuestHandedIn(pickupQuest))
                {
                    gameObject.SetActive(false);
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
                    gameObject.SetActive(false);
                }
                return;
            }
        }
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
}
