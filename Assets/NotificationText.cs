using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationText : MonoBehaviour
{
    Tween textTween;
    public float textSpeed;
    public TextMeshProUGUI textObject;
    public float activationTime;

    public void Kill() {
        SetText("");
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        gameObject.SetActive(false);
        Debug.Log("Killing " + gameObject.name);
    }

    public void Display(string _text)
    {
        string text = "";
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }

        textObject.color = new Color(1, 1, 1, 1);

        textTween = DOTween.To(() => text, x => text = x, _text, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            SetText(text);
        }).OnComplete(() =>
        {
            textTween = textObject.DOColor(new Color(1, 1, 1, 0), 2f).SetDelay(2f).OnComplete(() => { gameObject.SetActive(false); });
        });
    }

    public void SetText(string newText)
    {
        textObject.text = newText;
    }
}
