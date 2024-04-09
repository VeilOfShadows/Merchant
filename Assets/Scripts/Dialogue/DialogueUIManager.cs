using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

//This script sets up the dialogue ui with 
public class DialogueUIManager : MonoBehaviour
{
    #region Parameters
    public static DialogueUIManager instance;

    [Header("Dialogue Information")]
    public CharacterDialogueController dialogueController;
    [SerializeField] DialogueSO currentDialogue;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI npcNameText;
    [SerializeField] Image npcIcon;
    [SerializeField] Sprite defaultIcon;
    [SerializeField] GameObject dialoguePanel;
    [SerializeField] DialogueChoicePanel dialogueChoicePanel;
    
    [Header("Text Info")]
    Tween textTween;
    [SerializeField] float textSpeed;
    [SerializeField] AudioSource textAudioSource;
    [SerializeField] float minTimeBetweenSounds;

    [Header("Colours")]
    [SerializeField] Color locationColour;
    [SerializeField] Color npcColour;
    [SerializeField] Color directionColour;
    #endregion

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //finds the first dialogue node from the dialoge scriptable ojbect
    public void StartDialogue(CharacterDialogueController activeDialogue) {
        PlayerManager.instance.DeactivateUI();

        dialogueChoicePanel.ClearButtons();
        dialogueText.text = "";
        npcNameText.text = "";
        StartCoroutine(AnimIntro(activeDialogue));
    }

    //sets up the dialogue panel and fills in the text letter by letter
    public IEnumerator AnimIntro(CharacterDialogueController activeDialogue) {
        dialoguePanel.SetActive(true);
        Animation anim = dialoguePanel.GetComponent<Animation>();

        yield return new WaitUntil(() => !anim.isPlaying);

        activeDialogue.dialogue.SelectDialogues();
        currentDialogue = activeDialogue.FindStartingDialogue();
        npcNameText.text = activeDialogue.dialogue.dialogueContainer.npcName;
        npcIcon.sprite = activeDialogue.dialogue.dialogueContainer.npcIcon;
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
    }

    //Resets the Dialogue Panel to it's inactive state 
    public void EndDialogue()
    {
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        textAudioSource.Stop();
        dialoguePanel.SetActive(false);
        npcIcon.sprite = defaultIcon;
    }

    //Sets up the dialogue panel with the next dialogue node from the dialogeu Scriptable Object
    public void NextDialogue(DialogueSO nextDialogue)
    {
        currentDialogue = nextDialogue;
        dialogueChoicePanel.SetupButtons(currentDialogue.choices.Count, currentDialogue);

        minTimeBetweenSounds = Random.Range(.08f, .1f);
        float lastSoundTime = -minTimeBetweenSounds;

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

    //Sets up the text of the dialogue, colouring words if they contain certain characters before them
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
