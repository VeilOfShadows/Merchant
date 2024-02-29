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

    public Color locationColour;
    public Color npcColour;
    public Color directionColour;


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

    //public void StartDialogueAnimation()
    //{
    //    dialoguePanel.SetActive(true);
    //}

    public void StartDialogue(CharacterDialogueController activeDialogue) {
        dialogueChoicePanel.ClearButtons();
        dialogueText.text = "";
        npcNameText.text = "";
        StartCoroutine(AnimIntro(activeDialogue));
        //activeDialogue.dialogue.SelectDialogues();
        ////dialoguePanel.SetActive(true);
        //currentDialogue = activeDialogue.FindStartingDialogue();
        //dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);

        //minTimeBetweenSounds = Random.Range(.08f, .1f);
        //float lastSoundTime = -minTimeBetweenSounds;

        //npcNameText.text = activeDialogue.dialogue.dialogueContainer.npcName;

        ////DOTween.Kill(textTween);
        //if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        //{
        //    textTween.Kill();
        //}
        //string text = "";
        //textTween = DOTween.To(() => text, x => text = x, currentDialogue.text, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        //{
        //    if (Time.time - lastSoundTime >= minTimeBetweenSounds)
        //    {
        //        textAudioSource.Play();
        //        lastSoundTime = Time.time;
        //    }
        //    SetDialogueText(text);
        //});
    }

    public IEnumerator AnimIntro(CharacterDialogueController activeDialogue) {
        dialoguePanel.SetActive(true);
        Animation anim = dialoguePanel.GetComponent<Animation>();

        yield return new WaitUntil(() => !anim.isPlaying);

        activeDialogue.dialogue.SelectDialogues();
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
        string[] words = text.Split(' ');
        string formattedText = "";

        foreach (string word in words)
        {
            string cleanWord = word.Substring(1);
            string punctuation = "";
            string color = "";

            if (word.StartsWith("*"))
            {
                color = ColorUtility.ToHtmlStringRGBA(npcColour);
            }
            else if (word.StartsWith("<"))
            {
                color = ColorUtility.ToHtmlStringRGBA(locationColour);
                //color = locationColour.ToString();
            }
            else if (word.StartsWith(">"))
            {
                color = ColorUtility.ToHtmlStringRGBA(directionColour);
            }

            if (cleanWord.EndsWith(".") || cleanWord.EndsWith(",") || cleanWord.EndsWith("?"))
            {
                punctuation = cleanWord.Substring(cleanWord.Length - 1);
                cleanWord = cleanWord.Substring(0, cleanWord.Length - 1);
            }

            if (color != "")
            {
                //formattedText += color + cleanWord + "</color>" + punctuation + " ";
                formattedText += "<color=#" + color + ">" + cleanWord + "</color>" + punctuation + " ";
            }
            else
            {
                formattedText += word + " ";
            }
        }

        dialogueText.text = formattedText;
    }        
}
