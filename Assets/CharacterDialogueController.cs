using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueController : MonoBehaviour
{
    public CharacterDialogue dialogue;
    public DialogueGraphSaveDataSO graph;
    public DialogueSO currentDialogue;
    //public DialogueNodeSaveData currentDialogue;
    public DialogueSO lastDialogue;

    public void UpdateDialogues() 
    { 
        
    }

    public DialogueSO ReturnNextDialogue(DialogueSO dialogueToCheck) 
    {
        if (dialogueToCheck.choices[0].nextDialogue == null)
        {
            return null;
        }
        return dialogueToCheck.choices[0].nextDialogue;
    }

    public DialogueSO FindStartingDialogue()
    {
        for (int i = 0; i < dialogue.dialogueContainer.ungroupedDialogues.Count; i++)
        {
            if (dialogue.dialogueContainer.ungroupedDialogues[i].isStartingDialogue)
            {
                //Debug.Log(dialogue.dialogueContainer.ungroupedDialogues[i].name);
                currentDialogue = dialogue.dialogueContainer.ungroupedDialogues[i];
                return currentDialogue;
            }
        }
        Debug.Log("ERROR: No starting dialogue in DialogueSO");
        return null;
    }
}
