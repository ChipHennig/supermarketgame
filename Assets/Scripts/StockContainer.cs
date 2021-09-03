using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockContainer : MonoBehaviour
{
    public int MaxStock;
    private int numStock;
    public int Stock
    {
        get
        {
            return numStock;
        }
        set
        {
            numStock = value;
            if (numStock < 0)
            {
                numStock = 0;
            }
            else if (numStock > MaxStock)
            {
                numStock = MaxStock;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Stock = MaxStock;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
