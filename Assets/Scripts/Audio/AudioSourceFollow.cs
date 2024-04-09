using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to have the audio follow the player when they enter a region with different sounds
public class AudioSourceFollow : MonoBehaviour
{
    [SerializeField] Transform trackedObject;

    void LateUpdate()
    {
        transform.position = trackedObject.transform.position;
    }
}
