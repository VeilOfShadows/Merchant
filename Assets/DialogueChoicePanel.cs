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

            buttons[i].Setup(currentDialogue.choices[i].text, currentDialogue.choices[i].nextDialogue, CheckForFunctions(currentDialogue));
        }
    }

    public bool CheckForFunctions(DialogueSO _currentDialogue) {
        if (_currentDialogue.so != null)
        {
            return true;
        }
        return false;
    }
}
