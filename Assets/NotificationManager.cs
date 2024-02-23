using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    public List<NotificationText> textObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayNotification(string text)
    {
        Debug.Log("DISPLAYING");
        for (int i = 0; i < textObject.Count; i++)
        {
            if (!textObject[i].gameObject.activeInHierarchy)
            {
                Debug.Log(textObject[i]);
                textObject[i].gameObject.SetActive(true);
                textObject[i].Display(text);
                return;
            }
        }
    }
}
