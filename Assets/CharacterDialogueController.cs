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

     void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentDialogue == null)
            {
                for (int i = 0; i < dialogue.dialogueContainer.ungroupedDialogues.Count; i++)
                {
                    if (dialogue.dialogueContainer.ungroupedDialogues[i].isStartingDialogue)
                    {
                        //Debug.Log(dialogue.dialogueContainer.ungroupedDialogues[i].name);
                        currentDialogue = dialogue.dialogueContainer.ungroupedDialogues[i];
                    }
                }
                //if (currentDialogue == null)
                //{
                //    for (int i = 0; i < dialogue.dialogueContainer.dialogueGroups.Count; i++)
                //    {
                //        if (dialogue.dialogueContainer.dialogueGroups[i].value.isStartingDialogue)
                //        {
                //            Debug.Log(dialogue.dialogueContainer.groupedDialogues[i].name);
                //        }
                //    }
                //    //currentDialogue = graph.nodes[0];
                //}
                //currentDialogue = graph.nodes[0];
            }
            else
            {
                lastDialogue = currentDialogue;
                currentDialogue = ReturnNextDialogue(lastDialogue);
            }
            if (currentDialogue == null)
            {
                Debug.Log("There is no more dialogue.");
            }
            else
            {
                Debug.Log(currentDialogue.text);
            }
            //Debug.Log(dialogue.dialogueContainer.ungroupedDialogues[0].text);
            //Debug.Log(dialogue.dialogueContainer.GetGroupedDialogueNames(dialogue.dialogueGroup, true).);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            //lastDialogue = currentDialogue;
            //currentDialogue = lastDialogue.choices[0].;
            //Debug.Log(currentDialogue.text);
            //Debug.Log(dialogue.dialogueContainer.ungroupedDialogues[0].text);
            //Debug.Log(dialogue.dialogueContainer.GetGroupedDialogueNames(dialogue.dialogueGroup, true).);
        }
    }

    public DialogueSO ReturnNextDialogue(DialogueSO dialogueToCheck) 
    {
        if (dialogueToCheck.choices[0].nextDialogue == null)
        {
            return null;
        }
        return dialogueToCheck.choices[0].nextDialogue;
    }
}
