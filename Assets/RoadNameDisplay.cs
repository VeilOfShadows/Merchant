using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoadNameDisplay : MonoBehaviour
{
    public static RoadNameDisplay instance;

    public Animation anim;
    Tween textTween;
    public TextMeshProUGUI textObject;
    public string roadName;
    public float textSpeed;
    public float vanishDelay;

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

        roadName = _roadName;
        textObject.gameObject.SetActive(true);
        DOTween.Complete(textTween);
        string text = "";
        textTween = DOTween.To(() => text, x => text = x, _roadName, textSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            SetText(text);
        });
        StartCoroutine(AnimationDelay());
    }

    public IEnumerator AnimationDelay()
    {
        yield return new WaitForSeconds(vanishDelay);
        anim.Play();
    }

    public void SetText(string newText)
    {
        textObject.text = newText;
    }
}
