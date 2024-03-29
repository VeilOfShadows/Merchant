using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseController : MonoBehaviour
{
    public GameObject[] Lights;

    public void ToggleLights(bool toggle) 
    {
        for (int i = 0; i < Lights.Length; i++)
        {
            Lights[i].SetActive(toggle);
        }
    }
}
