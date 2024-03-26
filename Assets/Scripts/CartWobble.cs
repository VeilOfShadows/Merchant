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
    public float xRotation;
    public float maxXRotation;
    Vector3 positionClamp;
    float zPos;

    private void Start()
    {
        zPos = transform.localPosition.z;
    }

    private void LateUpdate()
    {
        //Quaternion targetRotation = Quaternion.LookRotation(target.position - aim.position);
        //float xRotation = targetRotation.eulerAngles.x;
        Vector3 targetVector = (target.position - aim.position).normalized;
        //float xRotation = 0;
        if (aim.position.y > target.position.y)
        {
            if (!(xRotation > maxXRotation) || !(xRotation < -maxXRotation))
            {
                xRotation+= .3f;
            }
        }
        else
        {
            if (!(xRotation > maxXRotation) || !(xRotation < -maxXRotation))
            {
                xRotation -= .3f;
            }
        }
        //xRotation = scale * (aim.position.y - target.position.y);
        float zClamp;
        if (transform.eulerAngles.z > 50 || transform.eulerAngles.z < -50)
        {
            zClamp = 0;
        }
        else
        {
            zClamp = transform.eulerAngles.z;
        }
        //else if (zClamp < -50)
        //{
        //    zClamp -= 50;
        //}
        positionClamp.x = 0;
        positionClamp.y = transform.localPosition.y;
        positionClamp.z = zPos;

        transform.localPosition = positionClamp;
        
        transform.localRotation = Quaternion.Euler(xRotation, 0, zClamp);
    }
}
