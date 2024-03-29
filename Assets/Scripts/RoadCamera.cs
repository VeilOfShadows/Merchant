using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCamera : MonoBehaviour
{
    public GameObject camera;

    private void Start()
    {
        camera = transform.Find("Camera Parent").gameObject;
    }
}
