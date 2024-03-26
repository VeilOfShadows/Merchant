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

    private void Start()
    {
        agent.destination = followTarget.position;
    }

    private void Update()
    {
        if (follow) 
        {
            if (Vector3.Distance(transform.position, followTarget.position) < agent.stoppingDistance)
            {

            }
            else
            {
            }

            animator.SetFloat(Animator.StringToHash("Walk"), agent.velocity.magnitude/agent.speed);
            agent.destination = followTarget.position;
            //if (agent.hasPath)
            //{
            //}
        }
    }
}
