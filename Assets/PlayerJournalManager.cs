using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJournalManager : MonoBehaviour
{
    public static PlayerJournalManager instance;
    public PlayerQuestManager questManager;
    public GameObject questLog;
    public GameObject questInformation;
    public TextMeshProUGUI questInformationText;

    public List<QuestLogButton> activeTextObjects = new List<QuestLogButton>();
    public List<QuestLogButton> completeTextObjects = new List<QuestLogButton>();
    public List<QuestLogButton> handedInTextObjects = new List<QuestLogButton>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Deactivate() { 
        DisableTextObjects();
        questLog.SetActive(false);
        questInformation.SetActive(false);
    }

    public void Activate() {
        questLog.SetActive(true); 
        questInformation.SetActive(true); 
        DisableTextObjects();
        FillActiveTextObjects();
        FillCompletedTextObjects();
        FillHandedInTextObjects();
    }

    public void DisableTextObjects()
    {
        for (int i = 0; i < activeTextObjects.Count; i++)
        {
            activeTextObjects[i].gameObject.SetActive(false);
            completeTextObjects[i].gameObject.SetActive(false);
            handedInTextObjects[i].gameObject.SetActive(false);
        }
    }

    public void ShowQuestDetails(QuestSO quest) {
        questInformationText.text = quest.questDescription;
    }

    public void FillActiveTextObjects() {
        for (int i = 0; i < questManager.activeQuestList.Count; i++)
        {
            for (int j = 0; j < activeTextObjects.Count; j++)
            {
                if (!activeTextObjects[j].gameObject.activeInHierarchy)
                {
                    activeTextObjects[j].gameObject.SetActive(true);
                    activeTextObjects[j].Setup(questManager.activeQuestList[i]);
                    return;
                }
            }
        }
    }
    public void FillCompletedTextObjects()
    {
        for (int i = 0; i < questManager.completedQuestList.Count; i++)
        {
            for (int j = 0; j < activeTextObjects.Count; j++)
            {
                if (!activeTextObjects[j].gameObject.activeInHierarchy)
                {
                    activeTextObjects[j].gameObject.SetActive(true);
                    activeTextObjects[j].Setup(questManager.completedQuestList[i]);
                    //activeTextObjects[j].text = " - " + questManager.completedQuestList[i].questName;
                    return;
                }
            }
        }
    }
    public void FillHandedInTextObjects()
    {
        for (int i = 0; i < questManager.handinQuestList.Count; i++)
        {
            for (int j = 0; j < activeTextObjects.Count; j++)
            {
                if (!activeTextObjects[j].gameObject.activeInHierarchy)
                {
                    activeTextObjects[j].gameObject.SetActive(true);
                    activeTextObjects[j].Setup(questManager.handinQuestList[i]);
                    return;
                }
            }
        }
    }
}
