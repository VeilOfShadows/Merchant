using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMaskTrack : MonoBehaviour
{
    public Transform trackedObject;

    private void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.position = Camera.main.WorldToScreenPoint(trackedObject.position);
    }
}
