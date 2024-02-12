using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class OldDialogueGraph : EditorWindow
{
    private OldDialogueGraphView _graphView;
    private string _fileName = "New Narrative";

    [MenuItem("Graph/OLD/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    { 
        var window = GetWindow<OldDialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
    }

    private void ConstructGraphView() 
    {
        _graphView = new OldDialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar() 
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data"});
        toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data"});

        var nodeCreateButton = new Button(()=>{ _graphView.CreateNode("Dialogue Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name", "Ok");
            return;
        }

        var saveUtility = OldGraphSaveUtility.GetInstance(_graphView);
        if (save) 
        {
            saveUtility.SaveGraph(_fileName);
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }

    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
