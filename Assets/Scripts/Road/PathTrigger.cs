using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Splines;

public class PathTrigger : MonoBehaviour
{
    public SplineContainer spline;
    public Spline roadSpline;
    public Transform trigger;
    public float3 pos;
    float moveX = 0f;
    float moveZ = 0f;
    public float offset;

    NativeSpline native;
    float distance;
    float3 forward;
    float3 up;
    float3 right;
    Vector3 remappedForward = new Vector3(0, 0, 1);
    Vector3 remappedUp = new Vector3(0, 1, 0);
    Quaternion axisRemapRotation;

    private void Start()
    {
        roadSpline = spline.Splines[0];
    }

    void Update()
    {
        native = new NativeSpline(roadSpline);
        distance = SplineUtility.GetNearestPoint(roadSpline, transform.position, out float3 nearest, out float t);

         forward = Vector3.Normalize(roadSpline.EvaluateTangent(t));
         //up = roadSpline.EvaluateUpVector(t);
         right = math.cross(forward, new Vector3(0, 1, 0));

        if (Input.GetKey(KeyCode.W)) {
            moveX = offset;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveX = -offset;
        }
        else
        {
            moveX = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveZ = offset;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveZ = -offset;
        }
        else
        {
            moveZ = 0f;
        }

        pos = nearest + moveX * right + moveZ * forward;
        trigger.position = pos;
    }
}
