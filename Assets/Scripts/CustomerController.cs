using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private int numItemsLeft;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        numItemsLeft = Random.Range(1, 5);

        // Disabling auto-breaking allows for continuous movement
        // between points (ie the agent doesn't slow down as it 
        // approaches a destination point)
        agent.autoBraking = false;

        FindItem();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < .5f)
        {
            numItemsLeft--;
            if(numItemsLeft != 0) {
                FindItem();
            } else {
                agent.destination = GameObject.FindGameObjectWithTag("Door").transform.position;
            }
        }
    }

    void FindItem()
    {
        GameObject[] shelves = GameObject.FindGameObjectsWithTag("Shelf");
        agent.destination = GetRandomPointAlongCollider(
            shelves[Random.Range(0, shelves.Length)].GetComponent<BoxCollider>()
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide");
        if (other.gameObject.CompareTag("Door") && numItemsLeft == 0)
        {
            Destroy(gameObject);
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
                dims[i] *= (1 - (Random.Range(0, 2) * 2));
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
