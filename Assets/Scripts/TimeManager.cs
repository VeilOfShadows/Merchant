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
    public float speedUpSpeed;
    public bool isNight;
    public RectTransform clockRect;
    public Vector2 clockXAnchors;
    public int currentHour;
    bool initial = false;

    public delegate void OnHourChangeDelegate(int newHour);
    public OnHourChangeDelegate onHourChange;

    //public int currentHour = 8;
    //public int currentMinute = 00;
    //public int targetHour;
    //public int targetMinute;
    //public int startHours;
    //public int startMinutes;
    //public int timeToAdd;
    //public float tweenSpeed;

    //public TextMeshProUGUI clockDisplay;
    //public RectTransform clockMaster;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        onHourChange = Test;
        Advance(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Advance(Time.deltaTime * speedUpSpeed);
        }
    }

    
    public void Advance(float amount)
    {
        if (currentHour != Mathf.FloorToInt(time))
        {
            currentHour = Mathf.FloorToInt(time);
            onHourChange(currentHour);
        }

        amount /= timeAdvancementSpeed;
        time += amount;
        time %= 24;

        if (!isNight)
        {
            if (time < 5 || time > 19)
            {
                isNight = true;
                PlayerManager.instance.playerCartFire.SetActive(true);
            }
        }
        else
        {
            if (time > 5 && time < 19)
            {
                isNight = false;
                PlayerManager.instance.playerCartFire.SetActive(false);
            }
        }

        float xpos = Mathf.Lerp(clockXAnchors.x, clockXAnchors.y, time/24);
        clockRect.anchoredPosition = new Vector2(xpos, 0);

        //clockMaster.DOLocalRotate(new Vector3(0, 0, (time / 24) * 360f), 1f);
        lightingManager.UpdateLighting(time/24f);
    }

    public void Test(int val) {
        if (initial)
        {
            PlayerHungerManager.instance.DepleteHunger();
        }
        initial = true;
    }
}
