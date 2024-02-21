using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SignpostText : MonoBehaviour
{
    Tween scaleTween;

    public void ScaleUp() { 
        DOTween.Complete(scaleTween);
        RectTransform rect = GetComponent<RectTransform>();
        scaleTween = rect.DOScale(Vector3.one * .01f, .4f).From();
    }
}
