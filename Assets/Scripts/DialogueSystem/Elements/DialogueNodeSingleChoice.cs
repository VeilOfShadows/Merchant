#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

public class DialogueNodeSingleChoice : DialogueNode
{
    public override void Initialise(string nodeName, DialogueGraphView dialogueGraphView, Vector2 position)
    {
        base.Initialise(nodeName, dialogueGraphView, position);

        dialogueType = DialogueType.SingleChoice;

        DialogueChoiceSaveData choiceData = new DialogueChoiceSaveData()
        {
            text = "Next Dialogue"
        };

        choices.Add(choiceData);
    }

    public override void Draw()
    {
        base.Draw();

        foreach (DialogueChoiceSaveData choice in choices) 
        {
            Port choicePort = this.CreatePort(choice.text);

            choicePort.userData = choice;

            outputContainer.Add(choicePort);

        }
        RefreshExpandedState();
    }
}
#endif