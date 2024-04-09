using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoadNameDisplay : MonoBehaviour
{
    public static RoadNameDisplay instance;

    public PlayerControls playerControls;
    public Animation anim;
    Tween textTween;
    Tween fadeTween;
    public TextMeshProUGUI textObject;
    public string roadName;
    public float textSpeed;
    public float vanishDelay;
    bool coroutineRunning = false;
    public AudioSource textAudioSource;
    public float minTimeBetweenSounds;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Display(string _roadName)
    {
        if (_roadName == roadName)
        {
            return;
        }

        minTimeBetweenSounds = Random.Range(.08f, .1f);
        float lastSoundTime = -minTimeBetweenSounds;

        roadName = _roadName;
        //anim.Play("RoadNameIntro");
        //textObject.gameObject.SetActive(true);
        //textObject.DOColor(new Color(1, 1, 1, 1), .1f);
        //string text = "";
        //textTween = DOTween.To(() => text, x => text = x, _roadName, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        //{
        //    DOTween.Complete(fadeTween);
        //    SetText(text);
        //}).OnComplete(() => 
        //{
        //    fadeTween = textObject.DOColor(new Color(1,1,1,0), 2f);
        //});
        string text = "";
        if (fadeTween != null && fadeTween.IsActive() && fadeTween.IsPlaying())
        {
            fadeTween.Kill();
        }
        if (textTween != null && textTween.IsActive() && textTween.IsPlaying())
        {
            textTween.Kill();
        }

        textObject.color = new Color(1, 1, 1, 1); 

        textTween = DOTween.To(() => text, x => text = x, _roadName, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            if (Time.time - lastSoundTime >= minTimeBetweenSounds)
            {
                textAudioSource.Play();
                lastSoundTime = Time.time;
            }
            SetText(text);
        }).OnComplete(() =>
        {
            fadeTween = textObject.DOColor(new Color(1, 1, 1, 0), 2f).SetDelay(2f);
        });
    }

    public IEnumerator AnimationDelay(string _roadName)
    {
        //if (coroutineRunning)
        //{
        //    yield break;
        //}
        //coroutineRunning = true;
       
        yield return new WaitForSeconds(vanishDelay);
        anim.Play("RoadNameFade");
        yield return new WaitForSeconds(1);
        textObject.gameObject.SetActive(false);
        //coroutineRunning = false;
        //CheckName();
    }

    //public void CheckName() {
    //    if (playerControls.roadName != roadName)
    //    {
    //        StartCoroutine(AnimationDelay(roadName));
    //    }
    //}

    public void SetText(string newText)
    {
        textObject.text = newText;
    }
}
