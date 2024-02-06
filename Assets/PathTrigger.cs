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
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) {
            moveX = -4f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveX = 4f;
        }
        else
        {
            moveX = 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveZ = 4f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveZ = -4f;
        }
        else
        {
            moveZ = 0f;
        }
        //if (Input.GetKeyDown(key))
        //{
        //    collider.enabled = true;
        //}

        //if (Input.GetKeyUp(key))
        //{
        //    collider.enabled = false;
        //}
        pos = new Vector3(moveX, 0f, moveZ);
        trigger.localPosition = pos;
    }

    public void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.layer == 8)
        //{
        //    Debug.Log(other.name);
        //    player.SetRoad(other.GetComponentInParent<SplineContainer>());
        //}
    }
}
