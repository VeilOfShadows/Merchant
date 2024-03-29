using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FootstepManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> clipList = new List<AudioClip>();

    public void PlaySound() { 
        audioSource.clip = clipList[Random.Range(0, clipList.Count)];
        audioSource.pitch = Random.Range(0.8f,1.2f);
        audioSource.Play();
    }
}
