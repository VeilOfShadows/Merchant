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

            animator.SetFloat(Animator.StringToHash("Walk"), currentVelocity);
            agent.destination = followTarget.position;
            WheelL.Rotate(1* (agent.velocity.magnitude/2), 0,0);
            WheelR.Rotate(1* (agent.velocity.magnitude/2), 0,0);
        }
    }
}
