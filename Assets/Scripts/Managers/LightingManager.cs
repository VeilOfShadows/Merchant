using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Playables;
using DG.Tweening;

public class LightingManager : MonoBehaviour
{
    public Light sun;
    public Light moon;
    public LightingPreset preset;
    public Material cloudMaterial;
    Tween sunTween;

    public void UpdateLighting(float timePercent)
    { 
        RenderSettings.ambientLight = preset.ambientColour.Evaluate(timePercent);
        RenderSettings.fogColor = preset.fogColour.Evaluate(timePercent);

        if (sun != null)
        {
            sun.color = preset.directionalColour.Evaluate(timePercent);
            sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
            //sun.intensity = Mathf.Lerp(0, 3, sun.transform.rotation.eulerAngles.x/360);
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

    public void FadeSunIntensity() {
        if (sunTween != null && sunTween.IsActive() && sunTween.IsPlaying())
        {
            sunTween.Kill();
        }

        sunTween = DOTween.To(() => sun.intensity, x => sun.intensity = x, 0, 1f).SetEase(Ease.Linear);
    }
    public void IncreaseSunIntensity()
    {
        if (sunTween != null && sunTween.IsActive() && sunTween.IsPlaying())
        {
            sunTween.Kill();
        }

        sunTween = DOTween.To(() => sun.intensity, x => sun.intensity = x, 1, 1f).SetEase(Ease.Linear);
    }
}
