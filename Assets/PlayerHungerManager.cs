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
    public bool drainHunger = true;

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

    public void EatFirstFood() {
        InventorySlot foodItemSlot = PlayerManager.instance.playerInventory.FindFoodInInventory();
        if (foodItemSlot != null)
        {
            if (currentHunger + foodItemSlot.item.nutritionalValue > maxHunger)
            {
                NotificationManager.instance.DisplayNotification("You are not hungry enough to eat yet", false);
            }
            else
            {
                RestoreHunger(foodItemSlot.item.nutritionalValue);
                foodItemSlot.RemoveAmount(1);
            }
        }
        else
        {
            Debug.Log("No food in inventory.");
        }
    }

    public void EatSelectedFood(InventorySlot _foodItemSlot)
    {
        if (_foodItemSlot != null)
        {
            if (currentHunger + _foodItemSlot.item.nutritionalValue > maxHunger)
            {
                NotificationManager.instance.DisplayNotification("You are not hungry enough to eat this", false);
            }
            else
            {
                RestoreHunger(_foodItemSlot.item.nutritionalValue);
                _foodItemSlot.RemoveAmount(1);
            }
        }
        else
        {
            Debug.Log("No food in inventory.");
        }
    }

    public void RestoreHunger(float hungerRestored)
    {
        float startingHunger = currentHunger;
        float target = currentHunger + hungerRestored;

        currentHunger += hungerRestored;

        //if (currentHunger <= hungerThreshold)
        //{
            hungerSlider.targetGraphic.DOColor(Color.green, .4f).From();
        //    Die();
        //}

        sliderTween = DOTween.To(() => startingHunger, x => startingHunger = x, target, tweenSpeed).SetEase(Ease.Linear).OnUpdate(() =>
        {
            hungerSlider.value = startingHunger / maxHunger;
        });
    }

    public void DepleteHunger()
    {
        if (!drainHunger)
        {
            return;
        }

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
