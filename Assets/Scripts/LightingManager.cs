using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;

public class LightingManager : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public LightingPreset preset;
    public Material cloudMaterial;

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

        if(cloudMaterial != null)
        {
            cloudMaterial.color = preset.cloudColour.Evaluate(timePercent);
            cloudMaterial.SetColor("_EmissionColor", preset.cloudColour.Evaluate(timePercent));
        }
    }
}
