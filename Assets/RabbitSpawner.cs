using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitSpawner : MonoBehaviour
{
    public GameObject rabbitObject;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rabbitObject.transform.localPosition = Vector3.zero;
            rabbitObject.SetActive(true);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rabbitObject.SetActive(false);
        }
    }
}
