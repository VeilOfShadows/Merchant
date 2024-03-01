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
    public QuestDatabase questdatabase;
    public Animation anim;


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
        questInformationText.text = "Nothing to see here. Select a quest to view information about it.";

        questInformation.GetComponent<AudioSource>().Play();
        DisableTextObjects();
        FillQuestLog();
        anim.Play();
    }

    public void ToggleQuestLog() {
        if (questLog.activeInHierarchy)
        {
            Deactivate();
        }
        else 
        {
            Activate();
        }
    }

    public void FillQuestLog() {
        for (int i = 0; i < questdatabase.quests.Length; i++)
        {
            switch (questdatabase.quests[i].questStatus)
            {
                case QuestStatus.NotStarted:
                    break;
                case QuestStatus.Accepted:
                    if (!activeTextObjects[i].gameObject.activeInHierarchy)
                    {
                        activeTextObjects[i].gameObject.SetActive(true);
                        activeTextObjects[i].Setup(questdatabase.quests[i].quest);
                    }
                    break;
                case QuestStatus.Completed:
                    if (!completeTextObjects[i].gameObject.activeInHierarchy)
                    {
                        completeTextObjects[i].gameObject.SetActive(true);
                        completeTextObjects[i].Setup(questdatabase.quests[i].quest);
                    }
                    break;
                case QuestStatus.HandedIn:
                    if (!handedInTextObjects[i].gameObject.activeInHierarchy)
                    {
                        handedInTextObjects[i].gameObject.SetActive(true);
                        handedInTextObjects[i].Setup(questdatabase.quests[i].quest);
                    }
                    break;
                case QuestStatus.DEBUGFORCE:
                    break;
                default:
                    break;
            }
        }
    }
    
    public void DisableTextObjects()
    {
        for (int i = 0; i < activeTextObjects.Count; i++)
        {
            activeTextObjects[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < completeTextObjects.Count; i++)
        {
            completeTextObjects[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < handedInTextObjects.Count; i++)
        {
            handedInTextObjects[i].gameObject.SetActive(false);
        }
    }

    public void ShowQuestDetails(QuestSO quest) {
        questInformationText.text = quest.questDescription;
    }
}
