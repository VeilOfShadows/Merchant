using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEditor;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    public LightingManager lightingManager;
    [Range(0, 24)]
    public float time;
    public float timeAdvancementSpeed;
    public bool isNight;
    public Animation animation;
    float storedAnimationTime;
    public bool advancing;

    //public int currentHour = 8;
    //public int currentMinute = 00;
    //public int targetHour;
    //public int targetMinute;
    //public int startHours;
    //public int startMinutes;
    //public int timeToAdd;
    //public float tweenSpeed;

    public float timeSpeed = 60f;
    //public TextMeshProUGUI clockDisplay;
    public RectTransform clockMaster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        StartCoroutine(InitialSetup());
        //Advance(0);
        //animation.Stop();
        //animation.Sample();//AdvanceTime(0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PauseAnim();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            //advancing = true;
            Advance(Time.deltaTime * timeAdvancementSpeed);
            //AdvanceTime(timeToAdd);
        }
    }

    public IEnumerator InitialSetup() {
        Advance(0);
        
        yield return null;
        animation.Stop();
        animation.Sample();
    }

    //public void AdvanceTime(int minutes)
    //{
    //    startHours = currentHour;
    //    startMinutes = currentMinute;

    //    currentMinute += minutes;

    //    while (currentMinute >= 60)
    //    {
    //        currentMinute -= 60;
    //        currentHour++;
    //    }
    //    while (currentHour >= 24)
    //    {
    //        currentHour = 0;
    //    }

    //    targetHour = currentHour;
    //    targetMinute = currentMinute;

    //    DOTween.To(() => startHours, x => startHours = x, targetHour, tweenSpeed).SetEase(Ease.Linear).OnUpdate(() => {
    //        if (startHours < 10)
    //        {
    //            //clockDisplay.text = ("0" + startHours.ToString());

    //        }
    //        else
    //        {
    //            //clockDisplay.text = (startHours.ToString());
    //        }
    //    });

    //    DOTween.To(() => startMinutes, x => startMinutes = x, targetMinute, tweenSpeed).SetEase(Ease.Linear).OnUpdate(() => {
    //        if (startMinutes < 10)
    //        {
    //            //clockDisplay.text += (":0" + startMinutes.ToString());
    //            if (currentHour < 12)
    //            {
    //                //clockDisplay.text += " am";
    //            }
    //            else
    //            {
    //                //clockDisplay.text += " pm";
    //            }
    //        }
    //        else
    //        {
    //            //
    //            //clockDisplay.text += (":" + startMinutes.ToString());
    //            if (currentHour < 12)
    //            {
    //                //clockDisplay.text += " am";
    //            }
    //            else
    //            {
    //                //clockDisplay.text += " pm";
    //            }
    //        }
    //    });
    //    float angle1 = (currentHour / 24f) * 360;
    //    float angle2 = (currentMinute / 60f) * 15;

    //    float angle3 = angle1 + angle2;

    //    clockMaster.DOLocalRotate(new Vector3(0, 0, angle3), 1f);
    //    //clockMaster..localEulerAngles = new Vector3(0,0,angle3);
    //}


    public void PauseAnim() {        
        time %= 24;
        foreach (AnimationState state in animation)
        {
            state.time = (time / 24) * state.length;
        }
        storedAnimationTime = animation["Sky"].time;
        animation["Sky"].time = storedAnimationTime;

        animation.Play();
        animation.Stop();
        animation.Sample();
    }
    public void Advance(float amount)
    {
        amount /= timeAdvancementSpeed;
        time += amount;
        time %= 24;

        if (!isNight)
        {
            if (time < 5 || time > 19)
            {
                isNight = true;
                //PlayerManager.instance.playerCartFire.SetActive(true);
            }
        }
        else
        {
            if (time > 5 && time < 19)
            {
                isNight = false;
                //PlayerManager.instance.playerCartFire.SetActive(false);
            }
        }
        foreach (AnimationState state in animation)
        {
            state.time = (time / 24) * state.length;
        }
        storedAnimationTime = animation["Sky"].time;
        animation["Sky"].time = storedAnimationTime;
        
        animation.Play();
        
        //animation.Play(animation.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, (time/24));
        //animation.speed = 1 / (time / 24);
        //clockMaster.DOLocalRotate(new Vector3(0, 0, (time / 24) * 360f), 1f);
        lightingManager.UpdateLighting(time/24f);
        //SetTime
    }
}
