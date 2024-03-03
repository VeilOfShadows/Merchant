using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RespawnTrigger : MonoBehaviour
{
    public SplineContainer nearestRoad;
    public Transform respawnLocation;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {            
            PlayerManager.instance.respawnLocation = this;
        }
    }
}
