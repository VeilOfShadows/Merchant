using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class PathTrigger : MonoBehaviour
{
    public Collider collider;
    public KeyCode key;
    public PlayerControls player;
    public Transform trigger;
    public Vector3 pos;
    float moveX = 0f;
    float moveZ = 0f;
    public float offset;

    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            moveX = -offset;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveX = offset;
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

        pos.x = moveX;
        pos.z = moveZ;
        trigger.localPosition = pos;
    }
}
