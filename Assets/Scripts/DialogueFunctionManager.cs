using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Progress;

public enum DialogueActions { 
    None,
    OpenShop,
    AcceptQuest,
    CompleteQuest,
    //HandInQuest
}

public class DialogueFunctionManager : MonoBehaviour
{
    public static DialogueFunctionManager instance;
    public PlayerSOConnections connector;
    public QuestDatabase questDatabase;
    public Inventory playerInventory;

    private void Awake()
    {
        if (instance == null)
        { instance = this; }
    }

    public void Activate(DialogueActions action, QuestSO pickupQuest = null, QuestSO completeQuest = null)
    {
        switch (action)
        {
            case DialogueActions.None:
                break;

            case DialogueActions.OpenShop:
                OpenShop();
                break;

            case DialogueActions.AcceptQuest:
                questDatabase.SetQuestStatus(pickupQuest.questID, QuestStatus.Accepted);
                if (pickupQuest.questStartAction == QuestAction.Item)
                {
                    playerInventory.AddItem(pickupQuest.questStartItem.data, pickupQuest.questStartItemAmount);
                    NotificationManager.instance.DisplayNotification("+ " + pickupQuest.questStartItem.data.itemName + " x " + pickupQuest.questStartItemAmount, false);
                }
                break;

            case DialogueActions.CompleteQuest:
                if (completeQuest.questRequiredItem != null)
                {
                    playerInventory.FindItemInInventory(completeQuest.questRequiredItem.data).RemoveAmount(completeQuest.questRequiredItemAmount);
                    NotificationManager.instance.DisplayNotification("- " + completeQuest.questRequiredItem.data.itemName + " x " + completeQuest.questRequiredItemAmount, false);
                }

                questDatabase.SetQuestStatus(completeQuest.questID, QuestStatus.Completed);
                if (completeQuest.questCompleteAction == QuestAction.Item)
                {
                    playerInventory.AddItem(completeQuest.questCompleteItem.data, completeQuest.questCompleteItemAmount);
                    NotificationManager.instance.DisplayNotification("+ " + completeQuest.questCompleteItem.data.itemName + " x " + completeQuest.questCompleteItemAmount, false);
                }
                break;

            //case DialogueActions.HandInQuest:
            //    questDatabase.SetQuestStatus(handInQuest.questID, QuestStatus.HandedIn);
            //    break;

            default:
                break;
        }
    }

    public void OpenShop()
    {
        PlayerManager.instance.EnterShop();
    }
}