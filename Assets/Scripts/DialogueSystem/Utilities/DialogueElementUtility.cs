#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class DialogueElementUtility
{
    public static Foldout CreateFoldout(string title, bool collapsed = false)
    {
        Foldout foldout = new Foldout()
        {
            text = title,
            value = !collapsed
        };

        return foldout;
    }

    public static Button CreateButton(string text, Action onClick = null)
    {
        Button button = new Button(onClick)
        {
            text = text
        };

        return button;
    }

    public static PopupField<DialogueActions> CreateActionField(EventCallback<ChangeEvent<DialogueActions>> onValueChanged = null)
    {
        List<DialogueActions> enumValues = Enum.GetValues(typeof(DialogueActions)).Cast<DialogueActions>().ToList();

        PopupField<DialogueActions> actionField = new PopupField<DialogueActions>()
        {
            label = "Function",
            choices = enumValues
        };

        if (onValueChanged != null)
        {
            actionField.RegisterValueChangedCallback(onValueChanged);
        }

        return actionField;
    }
    public static EnumField CreateEnumField(string _label, EventCallback<ChangeEvent<Enum>> onValueChanged = null)
    {
        EnumField enumField = new EnumField()
        {
            label = _label,
            
            //objectType = typeof(ScriptableObject)
        };

        if (onValueChanged != null)
        {
            enumField.RegisterValueChangedCallback(onValueChanged);
        }

        return enumField;
    }

    public static ObjectField CreateObjectField(string _label,EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null)
    {
        ObjectField objectField = new ObjectField()
        {
            label = _label,
            objectType = typeof(ScriptableObject)
        };

        if (onValueChanged != null)
        {
            objectField.RegisterValueChangedCallback(onValueChanged);
        }

        return objectField;
    }

    public static ObjectField CreateDialogueObjectField(string _label, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null)
    {
        ObjectField objectField = new ObjectField()
        {
            label = _label,
            objectType = typeof(DialogueContainerSO)
        };

        if (onValueChanged != null)
        {
            objectField.RegisterValueChangedCallback(onValueChanged);
        }

        return objectField;
    }

    public static ObjectField CreateQuestObjectField(string _label, EventCallback<ChangeEvent<UnityEngine.Object>> onValueChanged = null)
    {
        ObjectField objectField = new ObjectField()
        {
            label = _label,
            objectType = typeof(QuestSO)
        };

        if (onValueChanged != null)
        {
            objectField.RegisterValueChangedCallback(onValueChanged);
        }

        return objectField;
    }

    public static PopupField<string> CreatePopupField(EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        PopupField<string> popupField = new PopupField<string>()
        { 
            label = "Method Name"
        };

        if (onValueChanged != null)
        {
            popupField.RegisterValueChangedCallback(onValueChanged);
        }

        return popupField;
    }

    public static Port CreatePort(this DialogueNode node, string portName = "", Orientation orientation = Orientation.Horizontal, Direction direction = Direction.Output, Port.Capacity capacity = Port.Capacity.Single)
    {
        Port port = node.InstantiatePort(orientation, direction, capacity, typeof(bool));

        port.portName = portName;

        return port;
    }

    public static TextField CreateTextField(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textField = new TextField()
        {
            value = value,
            label = label
        };

        if (onValueChanged != null)
        {
            textField.RegisterValueChangedCallback(onValueChanged);
        }

        return textField;
    }

    public static TextField CreateTextArea(string value = null, string label = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
    {
        TextField textArea = CreateTextField(value, label, onValueChanged);

        textArea.multiline = true;
        return textArea;
    }
}
#endif