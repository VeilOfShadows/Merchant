#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DialogueSearchWindow : ScriptableObject, ISearchWindowProvider
{
    DialogueGraphView graphView;
    Texture2D indentationIcon;
    public void Initialize(DialogueGraphView dialogueGraphView)
    {
        dialogueGraphView = graphView;

        indentationIcon = new Texture2D(1, 1);
        indentationIcon.SetPixel(0, 0, Color.clear);
        indentationIcon.Apply();
    }

    //populate the search menu
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        List<SearchTreeEntry> searchTreeEntries = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Element")),
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"), 1),
            new SearchTreeEntry(new GUIContent("Single Choice", indentationIcon))
            {
            level = 2,
            userData = DialogueType.SingleChoice
            },
            new SearchTreeEntry(new GUIContent("Multiple Choice", indentationIcon))
            {
            level = 2,
            userData = DialogueType.MultipleChoice
            },
            new SearchTreeGroupEntry(new GUIContent("Dialogue Group"), 1),
            new SearchTreeEntry(new GUIContent("Single Group", indentationIcon))
            {
                level = 2,
                userData = new Group()
            },
        };

        return searchTreeEntries;
    }

    //perform action on selection. returning true - window will close, false - window will stay open
    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        Vector2 localMousePosition = graphView.GetLocalMousePosition(context.screenMousePosition, true);
        switch (SearchTreeEntry.userData)
        {
            case DialogueType.SingleChoice:
                {
                    DialogueNodeSingleChoice singleChoiceNode = (DialogueNodeSingleChoice) graphView.CreateNode("DialogueName", DialogueType.SingleChoice, localMousePosition);
                    
                    graphView.AddElement(singleChoiceNode);
                    return true;
                }
            case DialogueType.MultipleChoice:
                {
                    DialogueNodeMultipleChoice multipleChoiceNode = (DialogueNodeMultipleChoice) graphView.CreateNode("DialogueName", DialogueType.MultipleChoice, localMousePosition);

                    graphView.AddElement(multipleChoiceNode); 
                    return true;
                }
            case Group _:
                {
                    graphView.CreateGroup("DialogueGroup", localMousePosition);
                    return true;
                }
            default:
                {
                    return false;
                }
        }
    }
}
#endif