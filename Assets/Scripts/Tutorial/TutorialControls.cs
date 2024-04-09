using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControls : MonoBehaviour
{
    public float timeHeld;
    public float timeGoal;

    private void Update()
    {
        if (timeHeld >= timeGoal)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D))
        {
            timeHeld += Time.deltaTime;
        }
        
    }
}
