using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    /* Dialogue Scriptable Objects */
    [SerializeField] public DialogueContainerSO dialogueContainer;
    [SerializeField] public DialogueGroupSO dialogueGroup;
    [SerializeField] public DialogueSO dialogue;

    /* Filters */
    [SerializeField] private bool groupedDialogues;
    [SerializeField] private bool startingDialoguesOnly;

    /* Indexes */
    [SerializeField] private int selectedDialogueGroupIndex;
    [SerializeField] private int selectedDialogueIndex;
}
