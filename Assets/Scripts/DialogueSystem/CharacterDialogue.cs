using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogue : MonoBehaviour
{
    /* Dialogue Scriptable Objects */
    [SerializeField] private DialogueContainerSO dialogueContainer;
    [SerializeField] private DialogueGroupSO dialogueGroup;
    [SerializeField] private DialogueSO dialogue;

    /* Filters */
    [SerializeField] private bool groupedDialogues;
    [SerializeField] private bool startingDialoguesOnly;

    /* Indexes */
    [SerializeField] private int selectedDialogueGroupIndex;
    [SerializeField] private int selectedDialogueIndex;
}
