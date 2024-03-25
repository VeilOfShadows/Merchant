using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    Tween rotationTween;
    public TerrainController terrain;
    public Transform parent;
    public Animator animator;
    public Vector2 waitRange;
    bool isMoving = false;
    public float jumpRange;
    public float jumpDuration;
    Vector3 targetPosition;
    public float maxDistance;
    float distance;

    public void OnEnable()
    {
        Jump();
    }

    public void OnDisable()
    {
        rotationTween.Kill();
        StopAllCoroutines();
        isMoving = false;
    }

    public void Jump() {

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
            Debug.Log("CLOSE");
            rotationTween = transform.DOLocalRotate(new Vector3(parent.eulerAngles.x, Random.Range(0, 360), parent.eulerAngles.z), .6f).OnComplete(() =>
            {
                targetPosition = transform.localPosition + transform.forward * jumpRange;
                StartCoroutine(PerformJump());
            });
        }

        //parent.Rotate(new Vector3(parent.eulerAngles.x, Random.Range(0, 360), parent.eulerAngles.z));
        //animator.SetTrigger("Jump");
        //targetPosition = parent.position + parent.forward * jumpRange;
        //targetPosition.y = terrain.FindHeight(targetPosition) - 46.7f;
    }

    public IEnumerator PerformJump() {
        animator.SetTrigger("Jump");
        isMoving = true;

        rotationTween = transform.DOLocalMove(targetPosition, jumpDuration).OnComplete(() => isMoving = false); ;

        yield return new WaitUntil(() => !isMoving);

        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        Jump();
    }
}
