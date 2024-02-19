using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueChoicePanel : MonoBehaviour
{
    public List<DialogueChoiceButton> buttons = new List<DialogueChoiceButton>();

    public void SetupButtons(int amount, DialogueSO currentDialogue)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < amount; i++) 
        {
            buttons[i].gameObject.SetActive(true);

            buttons[i].Setup(currentDialogue.choices[i].text, currentDialogue.choices[i].nextDialogue, CheckForFunctions(currentDialogue, currentDialogue.choices[i].functionObject), currentDialogue.choices[i].methodName, currentDialogue.choices[i].questStartingPoint, currentDialogue.choices[i].questHandinPoint);
        }
    }

    public bool CheckForFunctions(DialogueSO _currentDialogue, ScriptableObject connector = null) {
        if (connector != null)
        {
            return true;
        }
        return false;
    }

}
