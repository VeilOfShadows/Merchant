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
    //public string functionName
    public void Setup(string text, DialogueSO nextDialogueSO, bool function)
    {
        isEnd = false;
        hasFunction = function;
        nextDialogue = nextDialogueSO;
        //if (dialogueUIManager.currentDialogue.quest != null)
        //{
        //    isQuest = true;
        //    GetComponentInChildren<TextMeshProUGUI>().text = "Quest";
        //}
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
            if (hasFunction)
            {
                DialogueFunctionManager.instance.Activate(DialogueUIManager.instance.currentDialogue.methodname);            
            }
            dialogueUIManager.EndDialogue();
            PlayerManager.instance.ExitShop();
            PlayerManager.instance.currentVendor.ExitShop();
            return;
        }
        
        dialogueUIManager.NextDialogue(nextDialogue);



        //if (nextDialogue.so != null)
        //{
        //    //PlayerManager.instance.so.OpenShop();
        //}
        //else
        //{
        //}

    }
}
