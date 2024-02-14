using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System;

public class DialogueGroup : Group
{
    public string ID { get; set; }
    public string oldTitle { get; set; }
    Color defaultBorderColour;
    float defaultBorderWidth;

    public DialogueGroup(string groupTitle, Vector2 position)
    {
        ID = Guid.NewGuid().ToString();
        title = groupTitle;
        oldTitle = groupTitle;

        SetPosition(new Rect(position, Vector2.zero));

        defaultBorderColour = contentContainer.style.borderBottomColor.value;
        defaultBorderWidth = contentContainer.style.borderBottomWidth.value;
    }

    public void SetErrorStyle(Color colour)
    { 
        contentContainer.style.borderBottomColor = colour;
        contentContainer.style.borderBottomWidth = 2f;
    }

    public void ResetStyle()
    {
        contentContainer.style.borderBottomColor = defaultBorderColour;
        contentContainer.style.borderBottomWidth = defaultBorderWidth;
    }
}
