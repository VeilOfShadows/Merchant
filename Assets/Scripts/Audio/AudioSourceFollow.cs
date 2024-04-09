using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceFollow : MonoBehaviour
{
    public Transform trackedObject;

    private void LateUpdate()
    {
        transform.position = trackedObject.transform.position;
    }
}
