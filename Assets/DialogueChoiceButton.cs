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
    public bool hasFunction = false;
    public bool hasQuestHandin = false;
    public QuestSO pickupQuest;
    public QuestSO handinQuest;
    public string functionName;

    public void Setup(string text, DialogueSO nextDialogueSO, bool function, string _functionName, QuestSO _pickupQuest, QuestSO _handinQuest)
    {
        isEnd = false;
        hasFunction = function;
        nextDialogue = nextDialogueSO;
        functionName = _functionName;
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
            if (PlayerQuestManager.instance.CheckQuestAvailable(pickupQuest))
            {
                Debug.Log("Quest is unavailable");

                gameObject.SetActive(false);
                return;
            }
        }

        if (handinQuest != null)
        {
            if (PlayerQuestManager.instance.CheckQuestAvailable(handinQuest))
            {
                Debug.Log("Quest is unavailable");

                gameObject.SetActive(false);
                return;
            }
        }
    }

    public void ActivateButton()
    {
        if (hasFunction)
        {
            if (pickupQuest != null)
            {
                DialogueFunctionManager.instance.AcceptQuest(pickupQuest);

                //DialogueFunctionManager.instance.AcceptQuest(pickupQuest);
            }
            else if (handinQuest != null)
            {
                DialogueFunctionManager.instance.HandInQuest(handinQuest);
            }
            else
            {
                DialogueFunctionManager.instance.Activate(functionName);
            }
        }

        if (isEnd)
        {
            dialogueUIManager.EndDialogue();
            PlayerManager.instance.ExitShop();
            PlayerManager.instance.currentVendor.ExitShop();
            return;
        }
        
        dialogueUIManager.NextDialogue(nextDialogue);
    }
}
