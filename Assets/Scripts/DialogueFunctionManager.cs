using UnityEngine;
//using static UnityEditor.Progress;

public enum DialogueActions { 
    None,
    OpenShop,
    AcceptQuest,
    CompleteQuest,
    OpenUpgrades,
    //HandInQuest
}

public class DialogueFunctionManager : MonoBehaviour
{
    public static DialogueFunctionManager instance;
    public PlayerSOConnections connector;
    public QuestDatabase questDatabase;

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
                if (pickupQuest.startActions.Length > 0)
                {
                    for (int i = 0; i < pickupQuest.startActions.Length; i++)
                    {
                        switch (pickupQuest.startActions[i].action)
                        {
                            case QuestAction.None:
                                break;

                            case QuestAction.Item:
                                PlayerManager.instance.playerInventory.AddItem(pickupQuest.startActions[i].item.data, pickupQuest.startActions[i].itemAmount);
                                NotificationManager.instance.DisplayNotification("+ " + pickupQuest.startActions[i].item.data.itemName + " x " + pickupQuest.startActions[i].itemAmount, false);
                                break;

                            case QuestAction.Function:
                                QuestFunctionManager.instance.Activate(pickupQuest.startActions[i].fuctionID);
                                break;

                            default:
                                break;
                        }
                        //if (pickupQuest.startActions[i].action == QuestAction.Item)
                        //{
                        //    playerInventory.AddItem(pickupQuest.startActions[i].item.data, pickupQuest.startActions[i].itemAmount);
                        //    NotificationManager.instance.DisplayNotification("+ " + pickupQuest.startActions[i].item.data.itemName + " x " + pickupQuest.startActions[i].itemAmount, false);
                        //}
                    }
                }
                break;

            case DialogueActions.CompleteQuest:
                if (completeQuest.itemRequirement.Length > 0)
                {
                    if (CanHandIn(completeQuest))
                    {
                        for (int i = 0; i < completeQuest.itemRequirement.Length; i++)
                        {
                            PlayerManager.instance.playerInventory.FindItemInInventory(completeQuest.itemRequirement[i].requiredItem.data).RemoveAmount(completeQuest.itemRequirement[i].requiredAmount);
                            NotificationManager.instance.DisplayNotification("- " + completeQuest.itemRequirement[i].requiredItem.data.itemName + " x " + completeQuest.itemRequirement[i].requiredAmount, false);
                        }                        
                    }
                }

                questDatabase.SetQuestStatus(completeQuest.questID, QuestStatus.Completed);
                if (completeQuest.completeActions.Length > 0)
                {
                    for (int i = 0; i < completeQuest.completeActions.Length; i++)
                    {
                        switch (completeQuest.completeActions[i].action)
                        {
                            case QuestAction.None:
                                break;

                            case QuestAction.Item:
                                PlayerManager.instance.playerInventory.AddItem(completeQuest.completeActions[i].item.data, completeQuest.completeActions[i].itemAmount);
                                NotificationManager.instance.DisplayNotification("+ " + completeQuest.completeActions[i].item.data.itemName + " x " + completeQuest.completeActions[i].itemAmount, false);
                                break;

                            case QuestAction.Function:
                                QuestFunctionManager.instance.Activate(completeQuest.completeActions[i].fuctionID);
                                break;

                            default:
                                break;
                        }
                        //if (completeQuest.completeActions[i].action == QuestAction.Item)
                        //{
                        //    playerInventory.AddItem(completeQuest.completeActions[i].item.data, completeQuest.completeActions[i].itemAmount);
                        //    NotificationManager.instance.DisplayNotification("+ " + completeQuest.completeActions[i].item.data.itemName + " x " + completeQuest.completeActions[i].itemAmount, false);
                        //}
                    }
                }
                break;

            case DialogueActions.OpenUpgrades:
                OpenUpgrades();
                break;
            //case DialogueActions.HandInQuest:
            //    questDatabase.SetQuestStatus(handInQuest.questID, QuestStatus.HandedIn);
            //    break;

            default:
                break;
        }
    }

    public bool CanHandIn(QuestSO quest) {
        for (int i = 0; i < quest.itemRequirement.Length; i++)
        {
            if (!PlayerManager.instance.playerInventory.CheckForItem(quest.itemRequirement[i].requiredItem.data, quest.itemRequirement[i].requiredAmount))
            {
                return false;
            }
            else
            {
                Debug.Log("You have the required item: " + quest.itemRequirement[i].requiredItem);
            }
        }

        return true;
    }

    public void OpenShop()
    {
        PlayerManager.instance.EnterShop();
    }

    public void OpenUpgrades()
    {
        PlayerManager.instance.OpenUpgradeMenu();
    }
}