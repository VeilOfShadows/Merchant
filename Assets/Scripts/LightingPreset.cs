using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Lighting Preset", menuName = "Create/Lighting/New Lighting Preset")]
public class LightingPreset : ScriptableObject
{
    public Gradient ambientColour;
    public Gradient directionalColour;
    public Gradient directionalMoonColour;
    public Gradient fogColour;
    public Gradient cloudColour;
}
