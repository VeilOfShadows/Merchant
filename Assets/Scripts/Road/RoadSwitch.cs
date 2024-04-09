using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;
using Cinemachine;

public class RoadSwitch : MonoBehaviour
{
    public SplineContainer road;
    public RoadJunction junction;
    public bool isDefault;
    public CinemachineSmoothPath dolly;

    public void OnMouseDown()
    {
        //junction.SwitchRoad(gameObject, road.Splines[0]);
    }
}
