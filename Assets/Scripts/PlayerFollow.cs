using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public NavMeshAgent agent;
    public bool follow;
    public Animator animator;
    public Transform WheelL;
    public Transform WheelR;
    float currentVelocity;

    private void Start()
    {
        agent.destination = followTarget.position;
    }

    private void Update()
    {
        if (follow) 
        {
            currentVelocity = agent.velocity.magnitude / agent.speed;
            if (Vector3.Distance(transform.position, followTarget.position) < agent.stoppingDistance)
            {

            }
            else
            {
            }

            animator.SetFloat(Animator.StringToHash("Walk"), currentVelocity);
            //animator.SetFloat(Animator.StringToHash("Walk"), agent.velocity.magnitude/agent.speed);
            agent.destination = followTarget.position;
            WheelL.Rotate(1* (agent.velocity.magnitude/2), 0,0);
            WheelR.Rotate(1* (agent.velocity.magnitude/2), 0,0);
            //    cartWheel_L.Rotate(1 * wheelSpeed, 0, 0);//Vector3 dir = power * transform.forward;
            //    cartWheel_R.Rotate(1 * wheelSpeed, 0, 0);//Vector3 dir = power * transform.forward;
            //}
            //else
            //{
            //    cartWheel_L.Rotate(1 * wheelSpeed, 0, 0);//Vector3 dir = power * transform.forward;
            //    cartWheel_R.Rotate(1 * wheelSpeed, 0, 0);//Vector3 dir = power * transform.forward;
            //} //if (agent.hasPath)
            //{
            //}
        }
    }
}
