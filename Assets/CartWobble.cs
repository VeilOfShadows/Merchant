using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartWobble : MonoBehaviour
{
    public Transform target;
    public Transform aim;

    private void LateUpdate()
    {
        //if (aim.position != target.position)
        //{
        //    Vector3 targetVector = (target.position - aim.position).normalized;
        //    Vector3 newVector = new Vector3(targetVector.x,0,0);

        //    Quaternion targetRotation = Quaternion.LookRotation(newVector);

        //    transform.rotation = targetRotation;
        //}
    }
}
