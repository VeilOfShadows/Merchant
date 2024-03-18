using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    Tween fadeInTween;
    Tween fadeOutTween;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void FadeTransition(AudioSource fadeOutSource, AudioSource fadeInSource)
    {
        fadeInSource.volume = 0;
        fadeInSource.Play();
        fadeInTween = DOTween.To(() => fadeInSource.volume, x => fadeInSource.volume = x, 0.3f, 10f).SetEase(Ease.Linear);

        fadeOutTween = DOTween.To(() => fadeOutSource.volume, x => fadeOutSource.volume = x, 0f, 10f).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeOutSource.Stop();
        });
    }
}
