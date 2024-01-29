using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class PlayerControls : MonoBehaviour
{
    public SplineContainer roadSpline;
    public Spline currentRoad;
    //public Rigidbody rb;
    public float speed;

    NativeSpline native;
    float distance;
    Vector3 forward;
    Vector3 up;
    Vector3 remappedForward = new Vector3(0, 0, 1);
    Vector3 remappedUp = new Vector3(0, 1, 0);
    Quaternion axisRemapRotation;
    float moveHorizontal;

    private void Start()
    {
        currentRoad = roadSpline.Splines[0];
        native = new NativeSpline(roadSpline.Spline);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;

        forward = Vector3.Normalize(roadSpline.EvaluateTangent(t));
        up = roadSpline.EvaluateUpVector(t);

        //var remappedForward = new Vector3(0, 0, 1);
        //var remappedUp = new Vector3(0, 1, 0);
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
    }
    private void Update()
    {
        //rb.velocity = rb.velocity.magnitude * transform.forward;
        moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal != 0)
        {
            Move();
        }
    }

    public void Move()
    {        
        native = new NativeSpline(currentRoad);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;

        forward = Vector3.Normalize(currentRoad.EvaluateTangent(t));
        up = currentRoad.EvaluateUpVector(t);

        //var remappedForward = new Vector3(0, 0, 1);
        //var remappedUp = new Vector3(0, 1, 0);
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
        transform.Translate(new Vector3(0, 0, (moveHorizontal * speed) * Time.deltaTime));
        //Vector3 dir = power * transform.forward;
        //rb.AddForce(dir);
    }
}
