using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueErrorData
{
    public Color color { get; set;}

    public DialogueErrorData()
    {
        GenerateRandomColor();
    }

    public void GenerateRandomColor()
    {
        color = new Color32(
            (byte)Random.Range(65, 256),
            (byte)Random.Range(50, 176),
            (byte)Random.Range(50, 176),
            255);
        
    }
}
