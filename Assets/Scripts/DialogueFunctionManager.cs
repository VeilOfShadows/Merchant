using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogueActions { 
    None,
    OpenShop,
    AcceptQuest,
    HandInQuest
}

public class DialogueFunctionManager : MonoBehaviour
{
    public static DialogueFunctionManager instance;
    public PlayerSOConnections connector;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }

    public void Activate(DialogueActions action, QuestSO pickupQuest = null, QuestSO handInQuest = null)
    {
        switch (action)
        {
            case DialogueActions.None:
                break;

            case DialogueActions.OpenShop:
                OpenShop();
                break;

            case DialogueActions.AcceptQuest:
                AcceptQuest(pickupQuest);
                break;

            case DialogueActions.HandInQuest:
                HandInQuest(handInQuest);
                break;

            default:
                break;
        }

        //test = DialogueUIManager.instance.currentDialogue.methodname;
        //Debug.Log(test);
        //Invoke(test, 0f);
    }

    public void OpenShop()
    {
        PlayerManager.instance.EnterShop();
    }

    public void AcceptQuest(QuestSO quest)
    {
        PlayerQuestManager.instance.AcceptQuest(quest);
    }

    public void HandInQuest(QuestSO quest)
    {
        PlayerQuestManager.instance.HandInQuest(quest);
    }
}