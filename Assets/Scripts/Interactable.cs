using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public class Interactable : MonoBehaviour
{
    public ItemObject item;
    public int amount;
    public Inventory playerInventory;
    public Transform interactableObject;
    public GameObject vfx;
    //public GameObject mesh;
    public Collider collider;
    Tween scaleTween;
    public float tweenSpeed = .4f;
    public AudioSource audioSource;

    public void OnMouseDown()
    {
        amount = 1;
        NotificationManager.instance.DisplayNotification("+ " + item.data.itemName + " x " + amount, false);

        collider.enabled = false;
        playerInventory.AddItem(item.data, amount);
        //vfx.SetActive(false);
        
        if (scaleTween != null && scaleTween.IsActive() && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }
        Vector3 originalScale = interactableObject.transform.localScale;
        //Transform transform = mesh.transform.parent;
        //interactableObject.localScale = Vector3.one;
        if (audioSource != null)
        {
            audioSource.Play();
        }

        scaleTween = interactableObject.DOPunchScale(originalScale * .2f, .4f).OnComplete(() => {
            scaleTween = interactableObject.DOScale(Vector3.one * .1f, tweenSpeed).OnUpdate(()=>
            {
                interactableObject.localPosition = Vector3.zero;
            }
            ).OnComplete(() => 
            {
                interactableObject.gameObject.SetActive(false);
                vfx.SetActive(false);
                interactableObject.localScale = originalScale; 
            });        
        });

        
        //mesh.SetActive(false);
    }
}
