using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    /* Dialogue Scriptable Objects */
    [SerializeField] public DialogueContainerSO dialogueContainer;
    [SerializeField] public DialogueContainerSO defaultDialogue;
    [SerializeField] public List<DialogueContainerSO> dialogueContainers;
    [SerializeField] public DialogueGroupSO dialogueGroup;
    [SerializeField] public DialogueSO dialogue;

    /* Filters */
    [SerializeField] private bool groupedDialogues;
    [SerializeField] private bool startingDialoguesOnly;

    /* Indexes */
    [SerializeField] private int selectedDialogueGroupIndex;
    [SerializeField] private int selectedDialogueIndex;

    public void SelectDialogues() {
        for (int i = 0; i < dialogueContainers.Count; i++)
        {
            for (int j = 0; j < dialogueContainers[i].prerequisiteQuests.Count; j++)
            {
                if (dialogueContainers[i].prerequisiteQuests[j].prerequisiteQuestProgressionRequirement == QuestStatus.DEBUGFORCE)
                {
                    dialogueContainer = dialogueContainers[i];
                    return;
                }
            }            
            //if (dialogueContainers[i].prere.prerequisiteQuestProgressionRequirement == QuestStatus.DEBUGFORCE)
            //{
            //    dialogueContainer = dialogueContainers[i];
            //    return;
            //}

            if (PlayerQuestManager.instance.FindAvailableDialogues(dialogueContainers[i].prerequisiteQuests))
            {
                dialogueContainer = dialogueContainers[i];
                Debug.Log("Quest has progressed enough. Dialogue is available");
                return;
            }
            else
            {
                dialogueContainer = defaultDialogue;
                Debug.Log("Quest has not progressed enough. Dialogue is unavailable");
            }
        }
    }
}
