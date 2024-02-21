using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DialogueUIManager : MonoBehaviour
{
    public static DialogueUIManager instance;
    public CharacterDialogueController dialogueController;
    public DialogueSO currentDialogue;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI npcNameText;

    public GameObject dialoguePanel;
    public DialogueChoicePanel dialogueChoicePanel;
    Tween textTween;
    public float textSpeed;
    public AudioSource textAudioSource;
    public float minTimeBetweenSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.M)) 
        //{
        //    StartDialogue();
        //}
    }

    public void StartDialogue(CharacterDialogueController activeDialogue) {
        activeDialogue.dialogue.SelectDialogues();
        dialoguePanel.SetActive(true);
        currentDialogue = activeDialogue.FindStartingDialogue();
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);

        minTimeBetweenSounds = Random.Range(.08f, .1f);
        float lastSoundTime = -minTimeBetweenSounds;

        npcNameText.text = activeDialogue.dialogue.dialogueContainer.npcName;

        //DOTween.Kill(textTween);
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        string text = "";
        textTween = DOTween.To(() => text, x => text = x, currentDialogue.text, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            if (Time.time - lastSoundTime >= minTimeBetweenSounds)
            {
                textAudioSource.Play();
                lastSoundTime = Time.time;
            }
            SetDialogueText(text);
        });
    }

    public void EndDialogue()
    {        
        dialoguePanel.SetActive(false);
    }

    public void NextDialogue(DialogueSO nextDialogue)
    {
        currentDialogue = nextDialogue;
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);

        minTimeBetweenSounds = Random.Range(.08f, .1f);
        float lastSoundTime = -minTimeBetweenSounds;

        //DOTween.Kill(textTween);
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        string text = "";
        textTween = DOTween.To(() => text, x => text = x, currentDialogue.text, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            if (Time.time - lastSoundTime >= minTimeBetweenSounds)
            {
                textAudioSource.Play();
                lastSoundTime = Time.time;
            }
            SetDialogueText(text);
        });
        //SetDialogueText(currentDialogue.text);
    }

    public void SetDialogueText(string text)
    {
        dialogueText.text = text;
    }
}
