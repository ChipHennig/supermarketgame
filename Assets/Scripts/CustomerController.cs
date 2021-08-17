using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{

    public Transform[] points;
    private int destPoint = 0;
    private NavMeshAgent agent;
    private bool isShopping;
    private int numItems;
    private int itemsBought;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        isShopping = true;

        // Disabling auto-breaking allows for continuous movement
        // between points (ie the agent doesn't slow down as it 
        // approaches a destination point)
        agent.autoBraking = false;

        GotoNextPoint();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GotoNextPoint();
        }
    }

    void GotoNextPoint()
    {
        // returns if no points have been set up
        // if (points.Length == 0)
        // {
        //     return;
        // }

        // agent.destination = points[destPoint].position;
        // destPoint = (destPoint + 1) % points.Length;

        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        agent.destination = GetRandomPointAlongCollider(
            shelves[Random.Range(0, shelves.Length)].GetComponent<BoxCollider>()
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door") && !isShopping)
        {
            Destroy(this.gameObject);
        }
    }

    public Vector3 GetRandomPointAlongCollider(BoxCollider boxCollider)
    {
        Vector3 extents = boxCollider.size / 2f;
        float[] dims = { extents.x, extents.y, extents.z };
        for (int i = 0; i < dims.Length; i++)
        {
            if (dims[i] == dims.Min())
            {
                dims[i] *= (1 - (Mathf.Round(Random.Range(0f, 1f)) * 2));
            }
            else
            {
                dims[i] = Random.Range(-dims[i], dims[i]);
            }
        }
        Vector3 point = new Vector3(dims[0], dims[1], dims[2]);

        return boxCollider.transform.TransformPoint(point);
    }
}
