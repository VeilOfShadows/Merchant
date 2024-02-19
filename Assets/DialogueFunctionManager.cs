using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueFunctionManager : MonoBehaviour
{
    public static DialogueFunctionManager instance;
    public PlayerSOConnections connector;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }

    public void Activate(string test)
    {
        //test = DialogueUIManager.instance.currentDialogue.methodname;
        Debug.Log(test);
        Invoke(test, 0f);
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