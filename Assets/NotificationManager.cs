using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class NotificationManager : MonoBehaviour
{
    public static NotificationManager instance;

    public AudioSource audioSource;
    public List<NotificationText> textObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayNotification(string text, bool playSound = true)
    {
        if (playSound)
        {
            audioSource.Play(); 
        }

        for (int i = 0; i < textObject.Count; i++)
        {
            if (!textObject[i].gameObject.activeInHierarchy)
            {
                textObject[i].gameObject.SetActive(true);
                textObject[i].Display(text);
                return;
            }
        }
    }
}
