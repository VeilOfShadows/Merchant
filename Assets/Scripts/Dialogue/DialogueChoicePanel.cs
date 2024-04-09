using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script coontrols the dialogue choice buttons, enabling and setting them up as needed
public class DialogueChoicePanel : MonoBehaviour
{
    [SerializeField] List<DialogueChoiceButton> buttons = new List<DialogueChoiceButton>();

    //reset all buttons to inactive
    public void ClearButtons() {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].Clear();
            buttons[i].transform.parent.gameObject.SetActive(false);
        }
    }

    //set up the buttons to sync details with the dialogue choices
    public void SetupButtons(int amount, DialogueSO currentDialogue)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].transform.parent.gameObject.SetActive(false);
        }

        for (int i = 0; i < amount; i++) 
        {
            buttons[i].transform.parent.gameObject.SetActive(true);

            buttons[i].Setup(currentDialogue.choices[i].text, currentDialogue.choices[i].nextDialogue, currentDialogue.choices[i].action, currentDialogue.choices[i].questStartingPoint, currentDialogue.choices[i].questCompletePoint, currentDialogue.choices[i].dialogueAfterCompletion);
        }
    }

    //Check if the dialogue node has any functions to apply to the button
    public bool CheckForFunctions(DialogueSO _currentDialogue, ScriptableObject connector = null) {
        if (connector != null)
        {
            return true;
        }
        return false;
    }

}
