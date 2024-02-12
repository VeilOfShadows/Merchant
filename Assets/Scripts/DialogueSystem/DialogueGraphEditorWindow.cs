using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogueGraphEditorWindow : EditorWindow
{
    string defaultFileName = "Type here...";
    Button saveButton;

    //Open the graph editor window through the toolbar at the top of unity
    [MenuItem("Graph/Dialogue Graph")]
    public static void Open() 
    {
        GetWindow<DialogueGraphEditorWindow>("Dialogue Graph");
    }

    private void OnEnable()
    {
        AddGraphView();
        AddToolbar();
    }

    private void AddGraphView()
    {
        DialogueGraphView graphView = new DialogueGraphView(this);

        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }
    
    private void AddToolbar()
    {
        Toolbar toolbar = new Toolbar();

        TextField fileNameTextField = DialogueElementUtility.CreateTextField(defaultFileName, "File Name:");

        saveButton = DialogueElementUtility.CreateButton("Save");

        toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);

        rootVisualElement.Add(toolbar);
    }

    public void EnableSaving()
    {
        saveButton.SetEnabled(true);
    }

    public void DisableSaving()
    {
        saveButton.SetEnabled(false);
    }
}
