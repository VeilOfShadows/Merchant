#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.IO;

public class DialogueGraphEditorWindow : EditorWindow
{
    DialogueGraphView graphView;
    string defaultFileName = "Type here...";
    Button saveButton;
    static TextField fileNameTextField;

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
        Button loadButton = DialogueElementUtility.CreateButton("Load", () => Load() );
        Button clearButton = DialogueElementUtility.CreateButton("Clear", () => Clear() );
        Button resetButton = DialogueElementUtility.CreateButton("Reset", () => ResetGraph() );

        toolbar.Add(fileNameTextField);
        toolbar.Add(saveButton);
        toolbar.Add(loadButton);
        toolbar.Add(clearButton);
        toolbar.Add(resetButton);

        rootVisualElement.Add(toolbar);
    }

    private void Load()
    {
        string filePath = EditorUtility.OpenFilePanel("Dialogue Graphs", "Assets/Editor/DialogueSystem/Graphs", "asset");

        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        Clear();

        DialogueIOUtility.Initialize(graphView, Path.GetFileNameWithoutExtension(filePath));
        DialogueIOUtility.Load();
    }

    private void ResetGraph()
    {
        Clear();

        UpdateFileName(defaultFileName);
    }

    public static void UpdateFileName(string newFileName) {
        fileNameTextField.value = newFileName;
    }

    private void Clear()
    {
        graphView.ClearGraph();
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
#endif