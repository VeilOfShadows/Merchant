using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogueGraphEditorWindow : EditorWindow
{
    DialogueGraphView graphView;
    string defaultFileName = "Type here...";
    Button saveButton;
    TextField fileNameTextField;

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
        graphView = new DialogueGraphView(this);

        graphView.StretchToParentSize();

        rootVisualElement.Add(graphView);
    }
    
    private void AddToolbar()
    {
        Toolbar toolbar = new Toolbar();

        fileNameTextField = DialogueElementUtility.CreateTextField(defaultFileName, "File Name:");

        saveButton = DialogueElementUtility.CreateButton("Save", () => Save() );
        toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);

        rootVisualElement.Add(toolbar);
    }

    private void Save()
    {
        if (string.IsNullOrEmpty(fileNameTextField.value))
        {
            EditorUtility.DisplayDialog("Invalid file name.", "Please ensure that the file name you have typed in is valid.", "Ok");

            return;
        }
        DialogueIOUtility.Initialize(graphView, fileNameTextField.value);
        DialogueIOUtility.Save();
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
