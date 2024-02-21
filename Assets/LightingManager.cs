using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public LightingPreset preset;
    public TimeManager timeManager;
    //[Range(0, 24)]
    //public float time;
    //public bool passiveAdvance = false;

    private void Update()
    {
        if (preset == null)
        {
            return;
        }

        //if (Application.isPlaying)
        //{
        //    if (passiveAdvance)
        //    {
        //        time += Time.deltaTime;
        //        time %= 24;
        //        timeManager.SetTime((time));
        //    }
        //    UpdateLighting(time / 24f);
        //}
        //else
        //{
        //    UpdateLighting(time / 24f);
        //}
    }

    public void UpdateLighting(float timePercent)
    { 
        RenderSettings.ambientLight = preset.ambientColour.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColour.Evaluate(timePercent);

        if (sun != null)
        {
            sun.color = preset.directionalColour.Evaluate(timePercent);
            sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

        if (moon != null)
        {
            moon.color = preset.directionalMoonColour.Evaluate(timePercent);
            moon.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) + 90f, 170f, 0));
        }
    }

    public void OnValidate()
    {
        //if (sun != null)
        //{
        //    return;
        //}

        //if (RenderSettings.sun != null)
        //{
        //    sun = RenderSettings.sun;
        //}
        //else
        //{
        //    Light[] lights = FindObjectsOfType<Light>();
        //    foreach (Light light in lights) 
        //    {
        //        sun = light;
        //        return;
        //    }
        //}
    }
}
