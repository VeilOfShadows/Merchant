using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script controls the day and night time audio
public class AudioTracker : MonoBehaviour
{
    [SerializeField] Transform playerLocation;
    [SerializeField] AudioSourceFollow activeAudioSource;
    [SerializeField] AudioSourceFollow daytimeAudio;
    [SerializeField] AudioSourceFollow nighttimeAudio;

    //Enables ambient sounds depending on the time of day
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
