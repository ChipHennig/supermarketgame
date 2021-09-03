using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public GameObject customerPrefab;
    private GameObject[] shelves;
    public GameObject textObj;
    private TextMeshProUGUI stockText;
    private StockContainer container;

    // Start is called before the first frame update
    void Start()
    {
        container = gameObject.GetComponent<StockContainer>();
        stockText = textObj.GetComponent<TextMeshProUGUI>();

        shelves = GameObject.FindGameObjectsWithTag("Shelf");
        InvokeRepeating("AddCustomer", 1, 6);
    }

    // Update is called once per frame
    void Update()
    {
        stockText.text = "Backroom stock: " + container.Stock + "/" + container.MaxStock;
    }

    void AddCustomer() {
        Instantiate(customerPrefab, 
            GameObject.FindGameObjectWithTag("Door").transform.position,
            Quaternion.identity);
    }
}
