using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartWobble : MonoBehaviour
{
    public Transform target;
    public Transform aim;
    public Transform wheel_L;
    public Transform wheel_R;
    public float scale;

    private void LateUpdate()
    {
        //Quaternion targetRotation = Quaternion.LookRotation(target.position - aim.position);
        //float xRotation = targetRotation.eulerAngles.x;
        Vector3 targetVector = (target.position - aim.position).normalized;
        float xRotation = scale * (aim.position.y - target.position.y);
        transform.rotation = Quaternion.Euler(xRotation, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
