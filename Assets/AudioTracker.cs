using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTracker : MonoBehaviour
{
    public Transform playerLocation;
    public AudioSourceFollow audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.trackedObject = playerLocation;
            audioSource.gameObject.SetActive(true);
        }
    }
}
