using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static System.Net.Mime.MediaTypeNames;

public class PlayerHungerManager : MonoBehaviour
{
    public static PlayerHungerManager instance;

    public Slider hungerSlider;
    Tween sliderTween;
    public float tweenSpeed;

    public float maxHunger;
    public float currentHunger;
    public float hourlyHungerDrain;
    public float hungerThreshold;

    private void Awake()
    {
        if (instance == null)
        { 
            instance = this; 
        }
    }

    private void Start()
    {
        hungerSlider.value = currentHunger / maxHunger;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            DepleteHunger();
        }
    }


    public void DepleteHunger()
    {
        float startingHunger = currentHunger;
        float target = currentHunger - hourlyHungerDrain;
        
        currentHunger -= hourlyHungerDrain;

        if (currentHunger <= hungerThreshold)
        {
            hungerSlider.targetGraphic.DOColor(Color.red, .4f).From();
            Die();
        }

        sliderTween = DOTween.To(() => startingHunger, x => startingHunger = x, target, tweenSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            hungerSlider.value = startingHunger / maxHunger;            
        });
    }

    public void Die() {
        Debug.Log("You are close to starving");
    }
}
