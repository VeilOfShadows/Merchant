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
    //public bool hasFunction =/* fals*/e;
    public DialogueActions action;
    public bool hasQuestHandin = false;
    public QuestSO pickupQuest;
    public QuestSO handinQuest;
    //public string functionName;

    public void Setup(string text, DialogueSO nextDialogueSO, DialogueActions _action, QuestSO _pickupQuest, QuestSO _handinQuest)
    {
        isEnd = false;
        //hasFunction = function;
        nextDialogue = nextDialogueSO;
        action = _action;
        //functionName = _functionName;
        pickupQuest = _pickupQuest;
        handinQuest = _handinQuest;

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
                Debug.Log("Quest is available");

                if (PlayerQuestManager.instance.CheckIfQuestCompleted(pickupQuest))
                {
                    Debug.Log("Quest already completed");
                    gameObject.SetActive(false);
                    return;
                }
                
                if (PlayerQuestManager.instance.CheckIfQuestHandedIn(pickupQuest))
                {
                    Debug.Log("Quest already completed");
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
                Debug.Log("Quest is available");
                if (!PlayerQuestManager.instance.CheckIfQuestCompleted(handinQuest))
                {
                    Debug.Log("Quest Not Completed");
                    gameObject.SetActive(false);
                }
                return;
            }
        }
    }

    public void ActivateButton()
    {
        DialogueFunctionManager.instance.Activate(action, pickupQuest, handinQuest);
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
