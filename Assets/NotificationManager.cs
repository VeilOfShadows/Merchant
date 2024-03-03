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
    NotificationText temp;
    float t;
    NotificationText oldest;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void DisplayNotification(string text, bool playSound = true, float duration = .5f)
    {

        if (playSound)
        {
            audioSource.Play(); 
        }

        temp = CheckAvailable();

        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            temp.Display(text, duration);
            temp.activationTime = Time.time;
        }
        else
        {
            temp = FindOldest();
            if (temp != null)
            {
                temp.gameObject.SetActive(true);
                temp.Display(text, duration);
                temp.activationTime = Time.time;
            }
        }

        t = 0;
        oldest = null;
        temp = null;
    }

    public NotificationText CheckAvailable() {
        for (int i = 0; i < textObject.Count; i++)
        {
            if (!textObject[i].gameObject.activeInHierarchy)
            {
                return textObject[i];
            }
        }
        return null;
    }

    public NotificationText FindOldest()
    {
        t = textObject[0].activationTime;
        oldest = textObject[0];
        for (int i = 0; i < textObject.Count; i++)
        {
            if (t > textObject[i].activationTime)
            {
                t = textObject[i].activationTime;
                oldest = textObject[i];
            }
        }
        if ( oldest != null )
        {
            oldest.Kill();
        }
        return oldest;
    }
}
