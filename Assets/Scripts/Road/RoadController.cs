using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    //public string roadName;
    public List<GameObject> triggers = new List<GameObject>();

    public void ActivateTriggers()
    {
        for (int i = 0; i < triggers.Count; i++)
        {
            triggers[i].SetActive(true);
        }
    }

    public void DeactivateTriggers() {
        for (int i = 0; i < triggers.Count; i++)
        {
            triggers[i].SetActive(false);
        }
    }
}
