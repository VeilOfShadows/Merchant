#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.UI;
using UnityEngine;


[CustomEditor(typeof(CharacterDialogue))]
public class DialogueInspector : Editor
{
    /* Dialogue Scriptable Objects */
    private SerializedProperty dialogueContainerProperty;
    private SerializedProperty defaultDialogueProperty;
    private SerializedProperty dialogueContainersProperty;
    private SerializedProperty dialogueGroupProperty;
    private SerializedProperty dialogueProperty;

    /* Filters */
    private SerializedProperty groupedDialoguesProperty;
    private SerializedProperty startingDialoguesOnlyProperty;

    /* Indexes */
    private SerializedProperty selectedDialogueGroupIndexProperty;
    private SerializedProperty selectedDialogueIndexProperty;

    private void OnEnable()
    {
        dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
        defaultDialogueProperty = serializedObject.FindProperty("defaultDialogue");
        dialogueContainersProperty = serializedObject.FindProperty("dialogueContainers");
        dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
        dialogueProperty = serializedObject.FindProperty("dialogue");

        groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
        startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");

        selectedDialogueGroupIndexProperty = serializedObject.FindProperty("selectedDialogueGroupIndex");
        selectedDialogueIndexProperty = serializedObject.FindProperty("selectedDialogueIndex");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawDialogueContainerArea();

        DialogueContainerSO currentDialogueContainer = (DialogueContainerSO)dialogueContainerProperty.objectReferenceValue;

        if (currentDialogueContainer == null)
        {
            StopDrawing("Select a Dialogue Container to see the rest of the Inspector.");

            return;
        }

        DrawFiltersArea();

        bool currentGroupedDialoguesFilter = groupedDialoguesProperty.boolValue;
        bool currentStartingDialoguesOnlyFilter = startingDialoguesOnlyProperty.boolValue;

        List<string> dialogueNames;

        string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{currentDialogueContainer.fileName}";

        string dialogueInfoMessage;

        if (currentGroupedDialoguesFilter)
        {
            List<string> dialogueGroupNames = currentDialogueContainer.GetDialogueGroupNames();

            if (dialogueGroupNames.Count == 0)
            {
                StopDrawing("There are no Dialogue Groups in this Dialogue Container.");

                return;
            }

            DrawDialogueGroupArea(currentDialogueContainer, dialogueGroupNames);

            DialogueGroupSO dialogueGroup = (DialogueGroupSO)dialogueGroupProperty.objectReferenceValue;

            dialogueNames = currentDialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);

            dialogueFolderPath += $"/Groups/{dialogueGroup.groupName}/Dialogues";

            dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Dialogues in this Dialogue Group.";
        }
        else
        {
            dialogueNames = currentDialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);

            dialogueFolderPath += "/Global/Dialogues";

            dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Ungrouped Dialogues in this Dialogue Container.";
        }

        if (dialogueNames.Count == 0)
        {
            StopDrawing(dialogueInfoMessage);

            return;
        }

        DrawDialogueArea(dialogueNames, dialogueFolderPath);

        serializedObject.ApplyModifiedProperties();
    }

    private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
    {
        DialogueInspectorUtility.DrawHelpBox(reason, messageType);

        DialogueInspectorUtility.DrawSpace();

        DialogueInspectorUtility.DrawHelpBox("You need to select a dialogue for this component to work properly at runtime!", MessageType.Warning);
        serializedObject.ApplyModifiedProperties();
        return;
    }

    #region Draw Methods
    private void DrawDialogueContainerArea()
    {
        DialogueInspectorUtility.DrawHeader("Dialogue Container");
        dialogueContainerProperty.DrawPropertyField();
        defaultDialogueProperty.DrawPropertyField();
        dialogueContainersProperty.DrawPropertyField();
        DialogueInspectorUtility.DrawSpace();
    }

    private void DrawFiltersArea()
    {
        DialogueInspectorUtility.DrawHeader("Filters");
        groupedDialoguesProperty.DrawPropertyField();
        startingDialoguesOnlyProperty.DrawPropertyField();
        DialogueInspectorUtility.DrawSpace();
    }

    private void DrawDialogueGroupArea(DialogueContainerSO dialogueContainer, List<string> dialogueGroupNames)
    {
        DialogueInspectorUtility.DrawHeader("Dialogue Group");

        int oldSelectedDialogueGroupIndex = selectedDialogueGroupIndexProperty.intValue;

        DialogueGroupSO oldDialogueGroup = (DialogueGroupSO)dialogueGroupProperty.objectReferenceValue;

        bool isOldDialogueGroupNull = oldDialogueGroup == null;

        string oldDialogueGroupName = isOldDialogueGroupNull ? "" : oldDialogueGroup.groupName;

        UpdateIndexOnNamesListUpdate(dialogueGroupNames, selectedDialogueGroupIndexProperty, oldSelectedDialogueGroupIndex, oldDialogueGroupName, isOldDialogueGroupNull);

        selectedDialogueGroupIndexProperty.intValue = DialogueInspectorUtility.DrawPopup("Dialogue Group", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

        string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueGroupIndexProperty.intValue];

        DialogueGroupSO selectedDialogueGroup = DialogueIOUtility.LoadAsset<DialogueGroupSO>($"Assets/DialogueSystem/Dialogues/{dialogueContainer.fileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

        dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;

        DialogueInspectorUtility.DrawDisabledFields(() => dialogueGroupProperty.DrawPropertyField());

        DialogueInspectorUtility.DrawSpace();
    }    

    private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
    {
        DialogueInspectorUtility.DrawHeader("Dialogue");

        int oldSelectedDialogueIndex = selectedDialogueIndexProperty.intValue;

        DialogueSO oldDialogue = (DialogueSO)dialogueProperty.objectReferenceValue;

        bool isOldDialogueNull = oldDialogue == null;

        string oldDialogueName = isOldDialogueNull ? "" : oldDialogue.dialogueName;

        UpdateIndexOnNamesListUpdate(dialogueNames, selectedDialogueIndexProperty, oldSelectedDialogueIndex, oldDialogueName, isOldDialogueNull);

        selectedDialogueIndexProperty.intValue = DialogueInspectorUtility.DrawPopup("Dialogue", selectedDialogueIndexProperty, dialogueNames.ToArray());

        string selectedDialogueName = dialogueNames[selectedDialogueIndexProperty.intValue];

        DialogueSO selectedDialogue = DialogueIOUtility.LoadAsset<DialogueSO>(dialogueFolderPath, selectedDialogueName);

        dialogueProperty.objectReferenceValue = selectedDialogue;
        DialogueInspectorUtility.DrawDisabledFields(() => dialogueProperty.DrawPropertyField());
    }
#endregion
    
    private void UpdateIndexOnNamesListUpdate(List<string> optionNames,SerializedProperty indexProperty, int oldSelectedPropertyIndex,string oldPropertyName, bool isOldPropertyNull)
    {
        if (isOldPropertyNull)
        {
            indexProperty.intValue = 0;

            return;
        }

        bool oldIndexIsOutOfBoundsOfNamesListCount = oldSelectedPropertyIndex > optionNames.Count - 1;
        bool oldNameIsDifferentThanSelectedName = oldIndexIsOutOfBoundsOfNamesListCount || oldPropertyName != optionNames[oldSelectedPropertyIndex];

        if (oldNameIsDifferentThanSelectedName)
        {
            if (optionNames.Contains(oldPropertyName))
            {
                indexProperty.intValue = optionNames.IndexOf(oldPropertyName);
            }
            else
            {
                indexProperty.intValue = 0;
            }
        }
    }
}
#endif