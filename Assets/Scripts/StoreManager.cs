using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject customerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddCustomer", 1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AddCustomer() {
        Instantiate(customerPrefab, 
            GameObject.FindGameObjectWithTag("Door").transform.position,
            Quaternion.identity);
    }
}
