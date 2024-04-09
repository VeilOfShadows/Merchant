using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotator : MonoBehaviour
{
   public float zRotation;

    private void Start()
    {
        transform.localEulerAngles = new Vector3(0, 0, zRotation);
    }
}
