using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RabbitController : MonoBehaviour
{
    public TerrainController terrain;
    public Transform parent;
    public Animator animator;
    public Vector2 waitRange;
    bool isMoving = false;
    public float jumpRange;
    public float jumpDuration;
    Vector3 targetPosition;

    private void Start()
    {
        //StartCoroutine(JumpDelay());
        Jump();
    }


    private void Update()
    {
        if (!isMoving) { 
            isMoving = true;
            Jump();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Jump();
        //}
    }
    public void Jump() {
        parent.Rotate(new Vector3(parent.eulerAngles.x, Random.Range(0, 360), parent.eulerAngles.z));
        animator.SetTrigger("Jump");
        targetPosition = parent.position + parent.forward * jumpRange;
        //targetPosition.y = terrain.FindHeight(targetPosition) - 46.7f;
    }

    //public IEnumerator JumpDelay() 
    //{
    //    //if (coroutineRunning) 
    //    //{
    //    //    yield break;
    //    //}
    //    //coroutineRunning = true;

    //    //yield return new WaitForSeconds(Random.Range(waitRange.x,waitRange.y));        

    //    //coroutineRunning = false;
    //    //Jump();
    //    //yield return null;
    //}

    private IEnumerator MoveGradually()
    {
        //isMoving = true;

        float elapsedTime = 0f;
        //float duration = 1f; // Adjust the duration as needed

        while (elapsedTime < jumpDuration)
        {
            // Interpolate the position
            parent.position = Vector3.Lerp(parent.position, targetPosition, elapsedTime / jumpDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(jumpDuration);

        // Ensure the final position matches the target
        parent.position = targetPosition;
        yield return new WaitForSeconds(Random.Range(waitRange.x, waitRange.y));
        //StartCoroutine(JumpDelay());
        isMoving = false;
        //Jump();
    }
}
