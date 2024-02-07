using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerFollow : MonoBehaviour
{
    public Transform followTarget;
    public NavMeshAgent agent;

    private void Start()
    {
        agent.destination = followTarget.position;
    }

    private void Update()
    {
        agent.destination = followTarget.position;
    }
}
