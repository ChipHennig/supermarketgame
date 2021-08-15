using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        // Disabling auto-breaking allows for continuous movement
        // between points (ie the agent doesn't slow down as it 
        // approaches a destination point)
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update() {
        if (!agent.pathPending && agent.remainingDistance < 0.5f) {
            GotoNextPoint();
        }
    }

    void GotoNextPoint() {
        // returns if no points have been set up
        if (points.Length == 0) {
            return;
        }

        agent.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length;
    }
}
