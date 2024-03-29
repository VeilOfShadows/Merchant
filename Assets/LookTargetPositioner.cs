using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LookTargetPositioner : MonoBehaviour
{
    public Transform targetTransform;
    public bool look;
    public float rotationSpeed;
    public Quaternion targetRotation;

    private void LateUpdate()
    {
        if (look)
        {
            
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            look = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            look = false;
        }
    }
}
