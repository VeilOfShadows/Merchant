using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.VFX;

public enum InteractableType { 
    DEFAULT,
    CollectItem,
    QuestItem,
    Greeble
}

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
    public Animation anim;
    public InteractableType interactableType;

    public void OnMouseDown()
    {
        switch (interactableType)
        {
            case InteractableType.DEFAULT:
                break;
            case InteractableType.CollectItem:
                if (Vector3.Distance(transform.position, PlayerManager.instance.transform.position) > 30)
                {
                    return;
                }
                CollectItem();
                
                break;

            case InteractableType.QuestItem:
                if (Vector3.Distance(transform.position, PlayerManager.instance.transform.position) > 30)
                {
                    return;
                }
                CollectItem(); 
                break;

            case InteractableType.Greeble:
                collider.enabled = false;
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                anim.Play();
                break;

            default:
                break;
        }
    }

    public void CollectItem(bool singleUse = false) 
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
            scaleTween = interactableObject.DOScale(Vector3.one * .1f, tweenSpeed).OnUpdate(() =>
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
    }

    public void EnableCollider() { 
        collider.enabled = true;
    }
}
