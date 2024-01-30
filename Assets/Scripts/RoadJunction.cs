using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class RoadJunction : MonoBehaviour
{
    public SplineContainer splineContainer;
    public SplineContainer splineContainer2;
    public List<GameObject> cameras = new List<GameObject>();

    //public List<int> roads = new List<int>();

    //public int currentRoad = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Debug.Log("SWITCHING ROAD");
            if (other.GetComponent<PlayerControls>().currentRoad == splineContainer.Splines[0])
            {
                other.GetComponent<PlayerControls>().currentRoad = splineContainer2.Splines[0];
                cameras[1].SetActive(true);
                cameras[0].SetActive(false);
            }
            else if(other.GetComponent<PlayerControls>().currentRoad == splineContainer2.Splines[0])
            {
                other.GetComponent<PlayerControls>().currentRoad = splineContainer.Splines[0];
                cameras[0].SetActive(true);
                cameras[1].SetActive(false);
            }

        }
    }
}
