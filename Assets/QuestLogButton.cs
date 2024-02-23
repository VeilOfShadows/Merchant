using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLogButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public QuestSO currentQuest;
    public Button button;

    private void Awake()
    {
        button.onClick.AddListener(Activate);
    }

    public void Setup(QuestSO _currentQuest) {
        currentQuest = null;
        currentQuest = _currentQuest;
        text.text = " - " + currentQuest.questName;
    }

    public void Activate() { 
        PlayerJournalManager.instance.ShowQuestDetails(currentQuest);
    }

}
