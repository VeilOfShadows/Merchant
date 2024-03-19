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
    public bool respawn = false;
    public bool onCooldown = false;
    public int cooldownTime;
    public int currentCooldown;
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
        PlayerManager.instance.playerInventory.AddItem(item.data, amount);
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
        currentCooldown = cooldownTime;
        onCooldown = true;
        if (respawn)
        {
            InteractableRespawnManager.instance.onCooldown.Add(this);
        }
    }

    public void EnableCollider() { 
        collider.enabled = true;
    }

    public void Respawn() {
        //todo - add respawn functionality

        Vector3 originalScale = interactableObject.transform.localScale;
        interactableObject.localScale = Vector3.zero;

        interactableObject.gameObject.SetActive(true);

        //scaleTween = interactableObject.DOScale(originalScale * .2f, .4f).OnComplete(() => {
            scaleTween = interactableObject.DOScale(originalScale, tweenSpeed * 2).OnUpdate(() =>
            {
                interactableObject.localPosition = Vector3.zero;
            }
            ).OnComplete(() =>
            {
                currentCooldown = 0;
                onCooldown = false;
                collider.enabled = true;
                vfx.SetActive(true);

                //interactableObject.localScale = originalScale;
            });
        //});

    }
}
