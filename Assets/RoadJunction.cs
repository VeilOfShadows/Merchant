using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadJunction : MonoBehaviour
{
    public SplineContainer splineContainer;

    //public List<int> roads = new List<int>();

    //public int currentRoad = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("SWITCHING ROAD");
            if (other.GetComponent<PlayerControls>().currentRoad == splineContainer.Splines[1])
            {
                other.GetComponent<PlayerControls>().currentRoad = splineContainer.Splines[0];
            }
            else if(other.GetComponent<PlayerControls>().currentRoad == splineContainer.Splines[0])
            {
                other.GetComponent<PlayerControls>().currentRoad = splineContainer.Splines[1];
            }

        }
    }
}
