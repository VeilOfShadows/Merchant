using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadTrigger : MonoBehaviour
{
    public GameObject cam;
    public PlayerControls playerControls;
    public RoadParent roadParent;

    public void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.layer == 8)
        {
            playerControls.SetRoad(roadParent.GetComponent<SplineContainer>(), cam);
        }
    }
}
