using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EntityController : MonoBehaviour
{
    protected NavMeshAgent agent;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
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
