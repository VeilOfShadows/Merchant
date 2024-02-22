using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Update()
    {
        if (isInIntro)
        {
            playerControls.TutorialMove();
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
}
