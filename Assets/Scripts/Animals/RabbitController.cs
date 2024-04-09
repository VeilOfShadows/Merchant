using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
//using UnityEditor.PackageManager;
using UnityEngine;

//This script is used to control the location and timing of the rabbit's jumps
public class RabbitController : MonoBehaviour
{
    [SerializeField] Transform parent;
    [SerializeField] Animator animator;
    [SerializeField] Vector2 waitRange;
    [SerializeField] float jumpRange;
    [SerializeField] float jumpDuration;
    [SerializeField] float maxDistance;
    
    Tween rotationTween;
    bool isMoving = false;
    float distance;
    Vector3 targetPosition;

    void OnEnable()
    {
        Jump();
    }

    void OnDisable()
    {
        rotationTween.Kill();
        StopAllCoroutines();
        isMoving = false;
    }

    //Assesses how far the rabbit is from their spawn location, jumping towards it if it is too far, and in a random direction otherwise
    void Jump() {

        distance = Vector3.Distance(transform.position, parent.transform.position);

        if (distance > maxDistance)
        {
            rotationTween = transform.DOLookAt(parent.transform.position, .6f).OnComplete(() =>
            {
                targetPosition = transform.localPosition + transform.forward * jumpRange;
                StartCoroutine(PerformJump());
            });
        }
        else
        {
            rotationTween = transform.DOLocalRotate(new Vector3(parent.eulerAngles.x, Random.Range(0, 360), parent.eulerAngles.z), .6f).OnComplete(() =>
            {
                targetPosition = transform.localPosition + transform.forward * jumpRange;
                StartCoroutine(PerformJump());
            });
        }
    }

    //used to handle the jump and wait before being able to jump again
    IEnumerator PerformJump() {
        animator.SetTrigger("Jump");
        isMoving = true;

        rotationTween = transform.DOLocalMove(targetPosition, jumpDuration).OnComplete(() => isMoving = false); ;

        yield return new WaitUntil(() => !isMoving);

        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        Jump();
    }
}
