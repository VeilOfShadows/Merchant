using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueNodeMultipleChoice : DialogueNode
{
    public override void Initialise(string nodeName, DialogueGraphView dialogueGraphView, Vector2 position)
    {
        base.Initialise(nodeName, dialogueGraphView, position);

        dialogueType = DialogueType.MultipleChoice;

        DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
        {
            text = "Next Dialogue"
        };

        choices.Add(choiceData);
    }

    public override void Draw()
    {
        base.Draw();

        //Create a button which allows you to add a choice to the dialogue
        Button addChoiceButton = DialogueElementUtility.CreateButton("Add Choice",() =>
        {
            DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
            {
                text = "Next Dialogue"
            };

            choices.Add(choiceData);

            Port choicePort = CreateChoicePort(choiceData);
            //PopupField<string> methodField = DialogueElementUtility.CreatePopupField(callback =>
            //{
            //    choiceData.methodName = callback.newValue;
            //});

            PopupField<DialogueActions> actionField = DialogueElementUtility.CreateActionField(callback =>
            {
                choiceData.action = callback.newValue;
            });

            if (choiceData.action != DialogueActions.None)
            {
                actionField.value = choiceData.action;
            }

            //ObjectField connectorSOObject = DialogueElementUtility.CreateObjectField("Function Object", callback =>
            //{
            //    //if (callback.newValue is ScriptableObject so)
            //    //{
            //    choiceData.functionObject = (ScriptableObject)callback.newValue;
            //    var methods = choiceData.functionObject.GetType().GetMethods().Select(m => m.Name).ToList();

            //    methodField.label = "Method Name";
            //    methodField.choices = methods;
            //    //}
            //});

            //if (choiceData.functionObject != null)
            //{
            //    connectorSOObject.value = choiceData.functionObject;
            //    var methods = choiceData.functionObject.GetType().GetMethods().Select(m => m.Name).ToList();

            //    methodField.label = "Method Name";
            //    methodField.choices = methods;

            //    methodField.value = choiceData.methodName;
            //}

            ObjectField questStartObjectField = DialogueElementUtility.CreateQuestObjectField("Quest Pick-up", callback =>
            {
                //if (callback.newValue is ScriptableObject so)
                //{
                choiceData.questStartingPoint = (QuestSO)callback.newValue;

                //}
            });

            if (choiceData.questStartingPoint != null)
            {
                questStartObjectField.value = choiceData.questStartingPoint;
            }

            ObjectField questHandInObjectField = DialogueElementUtility.CreateQuestObjectField("Quest Hand-in", callback =>
            {
                //if (callback.newValue is ScriptableObject so)
                //{
                choiceData.questHandinPoint = (QuestSO)callback.newValue;

                //}
            });

            if (choiceData.questHandinPoint != null)
            {
                questHandInObjectField.value = choiceData.questHandinPoint;
            }
            outputContainer.Add(choicePort);

            //outputContainer.Add(connectorSOObject);
            //outputContainer.Add(methodField);
            outputContainer.Add(questStartObjectField);
            outputContainer.Add(questHandInObjectField);
            outputContainer.Add(actionField);
        });

        addChoiceButton.AddToClassList("ds-node__button");
        mainContainer.Insert(1, addChoiceButton);

        

        //Create a set of choices and a button to delete them
        foreach (DialogueChoiceSaveData choice in choices)
        {
            Port choicePort = CreateChoicePort(choice);
            //PopupField<string> methodField = DialogueElementUtility.CreatePopupField(callback =>
            //{
            //    choice.methodName = callback.newValue;
            //});

            //ObjectField connectorSOObject = DialogueElementUtility.CreateObjectField("Function Object", callback =>
            //{
            //    //if (callback.newValue is ScriptableObject so)
            //    //{
            //    choice.functionObject = (ScriptableObject)callback.newValue;
            //    var methods = choice.functionObject.GetType().GetMethods().Select(m => m.Name).ToList();

            //    methodField.label = "Method Name";
            //    methodField.choices = methods;
            //    //}
            //});

            //if (choice.functionObject != null)
            //{
            //    connectorSOObject.value = choice.functionObject;
            //    var methods = choice.functionObject.GetType().GetMethods().Select(m => m.Name).ToList();

            //    methodField.label = "Method Name";
            //    methodField.choices = methods;

            //    methodField.value = choice.methodName;
            //}
            PopupField<DialogueActions> actionField = DialogueElementUtility.CreateActionField(callback =>
            {
                choice.action = callback.newValue;
            });

            if (choice.action != DialogueActions.None)
            {
                actionField.value = choice.action;
            }

            ObjectField questStartObjectField = DialogueElementUtility.CreateQuestObjectField("Quest Pick-up", callback =>
            {
                //if (callback.newValue is ScriptableObject so)
                //{
                choice.questStartingPoint = (QuestSO)callback.newValue;

                //}
            });

            if (choice.questStartingPoint != null)
            {
                questStartObjectField.value = choice.questStartingPoint;
            }

            ObjectField questHandInObjectField = DialogueElementUtility.CreateQuestObjectField("Quest Hand-in", callback =>
            {
                //if (callback.newValue is ScriptableObject so)
                //{
                choice.questHandinPoint = (QuestSO)callback.newValue;

                //}
            });

            if (choice.questHandinPoint != null)
            {
                questHandInObjectField.value = choice.questHandinPoint;
            }

            outputContainer.Add(choicePort);
            //outputContainer.Add(connectorSOObject);
            //outputContainer.Add(methodField);
            outputContainer.Add(actionField);
            outputContainer.Add(questStartObjectField);
            outputContainer.Add(questHandInObjectField);

        }
        RefreshExpandedState();
    }

    private Port CreateChoicePort(object userData)
    {        
        Port choicePort = this.CreatePort();

        choicePort.userData = userData;

        DialogueChoiceSaveData choiceData = (DialogueChoiceSaveData)userData;
        //choicePort.portName = "";

        Button deleteChoiceButton = DialogueElementUtility.CreateButton("X", () =>
        {
            if (choices.Count == 1)
            {
                return;
            }

            if (choicePort.connected)
            {
                graphView.DeleteElements(choicePort.connections);
            }

            choices.Remove(choiceData);
            graphView.RemoveElement(choicePort);
        });

        deleteChoiceButton.AddToClassList("ds-node__button");

        TextField choiceTextField = DialogueElementUtility.CreateTextField(choiceData.text, null, callback => 
        {
            choiceData.text = callback.newValue;
        });


        choiceTextField.AddToClassList("ds-node__textfield");
        choiceTextField.AddToClassList("ds-node__choice-textfield");
        //choiceTextField.AddToClassList("ds-node__choice-test-textfield");
        choiceTextField.AddToClassList("ds-node__textfield__hidden");

        choicePort.Add(choiceTextField);
        choicePort.Add(deleteChoiceButton);
        return choicePort;
    }
}
