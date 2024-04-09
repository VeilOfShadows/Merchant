using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SignpostText : MonoBehaviour
{
    Tween scaleTween;
    public GameObject canvas;

    public void ScaleUp() {
        if (scaleTween != null && scaleTween.IsActive() && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }

        RectTransform rect = GetComponent<RectTransform>();
        rect.localScale = Vector3.one * 0.1f;
        scaleTween = rect.DOScale(Vector3.one * .3f, .4f).OnComplete(() => { rect.localScale = Vector3.one * 0.3f; });
    }

    public void ScaleDown()
    {
        if (scaleTween != null && scaleTween.IsActive() && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }

        RectTransform rect = GetComponent<RectTransform>();
        scaleTween = rect.DOScale(Vector3.one * .01f, .4f).OnComplete(()=> { canvas.SetActive(false); rect.localScale = Vector3.one * 0.1f; });
    }
}
