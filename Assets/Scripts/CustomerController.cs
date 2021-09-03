using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerController : EntityController
{
    private int numItemsLeft;
    private GameObject nextShelf;

    protected override void Start()
    {
        base.Start();

        numItemsLeft = Random.Range(1, 5);

        // Disabling auto-breaking allows for continuous movement
        // between points (ie the agent doesn't slow down as it 
        // approaches a destination point)
        agent.autoBraking = false;

        FindItem();
    }

    protected override void Update()
    {
        base.Update();
        
        if (!agent.pathPending && agent.remainingDistance < .5f)
        {
            TakeItem();
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
        nextShelf = shelves[Random.Range(0, shelves.Length)];
        agent.destination = GetRandomPointAlongCollider(
            nextShelf.GetComponent<BoxCollider>()
        );
    }

    void TakeItem() {
        numItemsLeft--;
        nextShelf.GetComponent<StockContainer>().Stock--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Door") && numItemsLeft == 0)
        {
            Destroy(gameObject);
        }
    }

}
