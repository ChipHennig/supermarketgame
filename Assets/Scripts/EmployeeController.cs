using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EmployeeController : EntityController
{
    private bool doingTask;
    private GameObject chosenShelf;
    private StockContainer backStock;
    public string taskKey;

    protected override void Start()
    {
        base.Start();

        doingTask = false;
        backStock = GetComponentInParent<StockContainer>();
    }

    protected override void Update()
    {
        base.Update();

        if (!doingTask)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                {
                    agent.destination = hit.point;
                }
            }
            if (Input.GetKeyDown(taskKey))
            {
                doingTask = true;
                StartStock();
            }
        }
        else
        {
            if (!agent.pathPending && agent.remainingDistance < .5f
                && agent.hasPath)
            {
                agent.ResetPath();
                InvokeRepeating("DoStock", .5f, 1.5f);
            }
        }
    }

    void StartStock()
    {
        List<GameObject> shelves = GameObject.FindGameObjectsWithTag("Shelf").ToList();
        GameObject[] notFullShelves = shelves.Where(shelf =>
        {
            return (shelf.GetComponent<StockContainer>().Stock <
                shelf.GetComponent<StockContainer>().MaxStock);
        }).ToArray();
        if (notFullShelves.Length > 0)
        {
            chosenShelf = notFullShelves[Random.Range(0, notFullShelves.Length)];

            agent.destination = GetRandomPointAlongCollider(
                chosenShelf.GetComponent<BoxCollider>()
            );
        }
    }

    void DoStock()
    {
        if (chosenShelf.GetComponent<StockContainer>().Stock <
            chosenShelf.GetComponent<StockContainer>().MaxStock)
        {
            backStock.Stock--;
            if(backStock.Stock > 0) {
                chosenShelf.GetComponent<StockContainer>().Stock++;
            }
        }
        else
        {
            doingTask = false;
            CancelInvoke();
        }
    }

}
