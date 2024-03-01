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
    public NotificationText temp;

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

        temp = CheckAvailable();

        if (temp != null)
        {
            temp.gameObject.SetActive(true);
            temp.Display(text);
            temp.activationTime = Time.time;
        }
        else
        {
            temp = FindOldest();
            if (temp != null)
            {
                temp.gameObject.SetActive(true);
                temp.Display(text);
                temp.activationTime = Time.time;
            }
        }

        //for (int i = 0; i < textObject.Count; i++)
        //{
        //    if (!textObject[i].gameObject.activeInHierarchy)
        //    {
        //        textObject[i].gameObject.SetActive(true);
        //        textObject[i].Display(text);
        //        return;
        //    }
        //}
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
        Debug.Log("No available texts");
        return null;
    }

    public NotificationText FindOldest()
    {
        float t = textObject[0].activationTime;
        NotificationText oldest = textObject[0];
        //Debug.Log(t + " " + textObject[0]);
        for (int i = 0; i < textObject.Count; i++)
        {
            //Debug.Log("T: " + t + ". Object: " + textObject[i] + ". Activation Time: " + textObject[i].activationTime);
            if (t > textObject[i].activationTime)
            {
                t = textObject[i].activationTime;
                oldest = textObject[i];
            }
        }
        if ( oldest != null )
        {
            oldest.Kill();
            Debug.Log("Oldest: " + oldest.gameObject.name);
        }
        return oldest;
    }
}
