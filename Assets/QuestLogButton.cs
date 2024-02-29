using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class QuestLogButton : MonoBehaviour
{
    public TextMeshProUGUI text;
    public QuestSO currentQuest;
    public Button button;
    public AudioSource pageAudio;
    public AudioSource audioSource;
    Tween textTween;

    private void Awake()
    {
        button.onClick.AddListener(Activate);
    }

    public void Setup(QuestSO _currentQuest) {
        currentQuest = null;
        currentQuest = _currentQuest;
        text.text = " - " + currentQuest.questName;
    }

    public void Hover()
    {
        DOTween.Complete(textTween);
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOAnchorPosX(200, .3f);
        audioSource.Play();
    }
    public void Leave()
    {
        DOTween.Complete(textTween);
        RectTransform rect = this.GetComponent<RectTransform>();
        rect.DOAnchorPosX(190, .3f);
    }

    public void Activate() { 
        PlayerJournalManager.instance.ShowQuestDetails(currentQuest);
        pageAudio.pitch = Random.Range(.7f, 1.3f);
        pageAudio.Play();
        pageAudio.pitch = 1;
    }

}
