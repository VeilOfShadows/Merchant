using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Splines;

public class PlayerControls : MonoBehaviour
{
    public static PlayerControls instance;
    public SplineContainer roadSpline;
    public Spline currentRoad;
    public string roadName;
    //public Rigidbody rb;
    public float villageSpeed = 3;
    public float roadSpeed = 4.5f;
    public float currentSpeed;
    public NavMeshAgent agent;
    public Animator characterAnim;
    public Transform cart;
    public GameObject currentCam;
    public TimeManager timeManager;
    public PathTrigger trigger;
    public bool canControl;

    NativeSpline native;
    float distance;
    Vector3 forward;
    Vector3 up;
    Vector3 remappedForward = new Vector3(0, 0, 1);
    Vector3 remappedUp = new Vector3(0, 1, 0);
    Quaternion axisRemapRotation;
    float moveHorizontal;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetRoadSpeed();
        if (roadSpline != null)
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
    }
    private void Update()
    {
        if (canControl)
        { 
            moveHorizontal = Input.GetAxis("Horizontal");
            if (moveHorizontal != 0)
            {
                Move();
            }
        }
    }

    public void Move()
    {
        timeManager.Advance(Time.deltaTime);
        native = new NativeSpline(currentRoad);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;

        //cart.transform.position = transform.position - (Vector3.forward * 2);
        
        forward = Vector3.Normalize(currentRoad.EvaluateTangent(t));
        up = currentRoad.EvaluateUpVector(t);

        //var remappedForward = new Vector3(0, 0, 1);
        //var remappedUp = new Vector3(0, 1, 0);
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
        transform.Translate(new Vector3(0, 0, (moveHorizontal * currentSpeed) * Time.deltaTime));
        //transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
        //cart.Translate(new Vector3(0, 0, (moveHorizontal * speed) * Time.deltaTime));
        float direction = Input.GetAxis("Vertical");
        //rb.AddForce(dir);
    }

    public void TutorialMove()
    {
        //timeManager.Advance(Time.deltaTime);
        native = new NativeSpline(currentRoad);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;

        //cart.transform.position = transform.position - (Vector3.forward * 2);

        forward = Vector3.Normalize(currentRoad.EvaluateTangent(t));
        up = currentRoad.EvaluateUpVector(t);

        //var remappedForward = new Vector3(0, 0, 1);
        //var remappedUp = new Vector3(0, 1, 0);
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
        transform.Translate(new Vector3(0, 0, currentSpeed * Time.deltaTime));
    }

    public void MoveBackwards()
    {
        native = new NativeSpline(currentRoad);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;

        //cart.transform.position = transform.position - (Vector3.forward * 2);

        forward = Vector3.Normalize(currentRoad.EvaluateTangent(t));
        up = currentRoad.EvaluateUpVector(t);

        //var remappedForward = new Vector3(0, 0, 1);
        //var remappedUp = new Vector3(0, 1, 0);
        axisRemapRotation = Quaternion.Inverse(Quaternion.LookRotation(remappedForward, remappedUp));

        transform.rotation = Quaternion.LookRotation(forward, up) * axisRemapRotation;
        transform.Translate(new Vector3(0, 0, (moveHorizontal * (currentSpeed / 2)) * Time.deltaTime));
    }

    public void SetRoad(SplineContainer container, GameObject cam) {
        
        //roadName = _roadName;
        cam.SetActive(true);
        if (currentCam != cam)
        {
            currentCam.SetActive(false);
        }
        currentCam = cam;
        roadSpline = container;
        currentRoad = roadSpline.Splines[0];
        trigger.spline = roadSpline;
        trigger.roadSpline = currentRoad;
        native = new NativeSpline(currentRoad);
        distance = SplineUtility.GetNearestPoint(currentRoad, transform.position, out float3 nearest, out float t);

        transform.position = nearest;
    }

    public void SetVillageSpeed() {
        DOTween.To(() => currentSpeed, x => currentSpeed = x, villageSpeed, 2f).SetEase(Ease.Linear).OnUpdate(() => 
        { 
            agent.speed = currentSpeed;
        });
    }

    public void SetRoadSpeed()
    {
        DOTween.To(() => currentSpeed, x => currentSpeed = x, roadSpeed, 2f).SetEase(Ease.Linear).OnUpdate(() => 
        {
            agent.speed = currentSpeed;
            
        });
    }

    public void ChangeSpeed(float amount, bool increase)
    {
        if (increase)
        {
            currentSpeed += amount;
        }
        else
        {
            currentSpeed -= amount;
        }
        agent.speed = currentSpeed;
    }
}
