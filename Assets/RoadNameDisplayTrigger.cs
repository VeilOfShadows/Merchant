using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadNameDisplayTrigger : MonoBehaviour
{
    public string roadName;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8)
        {
            RoadNameDisplay.instance.Display(roadName);
        }
    }
}
