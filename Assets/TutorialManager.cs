using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class TutorialManager : MonoBehaviour
{
    public PlayerControls playerControls;
    public bool isInIntro = true;

    [Header("Trigger 1")]
    public GameObject introRoad;
    public GameObject controlsPanel;
    public GameObject trigger1;

    [Header("Trigger 2")]
    public GameObject trigger2;
    public GameObject berryPanel;
    public bool isStep2;

    [Header("Trigger 3")]
    public GameObject trigger3;
    public GameObject hungerPanel;
    public bool isStep3;

    private void Update()
    {
        if (isInIntro)
        {
            playerControls.TutorialMove();
        }
        if (isStep2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CancelAction2();
            }
        }
        if (isStep3)
        {
            if (Input.GetMouseButtonDown(0))
            {
                CancelAction3();
            }
        }
    }

    public void ActivateTutorialSequence(int index)
    {
        Invoke("Action" + index.ToString(), 0f);
    }

    public void Action1(){
        isInIntro = false;
        trigger1.SetActive(false);
        introRoad.SetActive(false);
        controlsPanel.SetActive(true);
        playerControls.canControl = true;

        trigger2.SetActive(true);        
    }

    public void Action2()
    {
        isStep2 = true;
        berryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CancelAction2() {
        isStep2 = false;
        berryPanel.SetActive(false);
        trigger2.SetActive(false);
        trigger3.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Action3()
    {
        isStep3 = true;
        hungerPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CancelAction3()
    {
        isStep3 = false;
        hungerPanel.SetActive(false);
        Time.timeScale = 1f;
        trigger3.SetActive(false);
    }
}
