using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is used to enable and disable the rabbit GameObject, depending on proximity to the player
public class RabbitSpawner : MonoBehaviour
{
    [SerializeField] GameObject rabbitObject;

    void Start()
    {
        rabbitObject.SetActive(false);
    }

    //enables the rabbit GameObject when player is nearby
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rabbitObject.transform.localPosition = Vector3.zero;
            rabbitObject.SetActive(true);
        }
    }

    //disables the rabbit GameObject when player is far away
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rabbitObject.SetActive(false);
        }
    }
}
