using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTracker : MonoBehaviour
{
    public Transform playerLocation;
    public AudioSourceFollow activeAudioSource;
    public AudioSourceFollow daytimeAudio;
    public AudioSourceFollow nighttimeAudio;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerManager.instance.currentAudioTrackerZone = this;
            if (TimeManager.instance.isNight)
            {
                NighttimeAudio();
            }
            else
            {
                DaytimeAudio();
            }
            //activeAudioSource.trackedObject = playerLocation;
            //daytimeAudio.gameObject.SetActive(true);
        }
    }

    public void DaytimeAudio() { 
        daytimeAudio.gameObject.SetActive(true);
        nighttimeAudio.gameObject.SetActive(false);
        activeAudioSource = daytimeAudio;
    }

    public void NighttimeAudio()
    {
        nighttimeAudio.gameObject.SetActive(true);
        daytimeAudio.gameObject.SetActive(false);
        activeAudioSource = nighttimeAudio;
    }
}
