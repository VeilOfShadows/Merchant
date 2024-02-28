using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadTrigger : MonoBehaviour
{
    public GameObject cam;
    public PlayerControls playerControls;
    public RoadController roadController;

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == 9)
        if (other.CompareTag("PlayerNode"))
        {
            playerControls.SetRoad(roadController.GetComponent<SplineContainer>(), cam/*, roadController.roadName*/);
        }
    }
}
