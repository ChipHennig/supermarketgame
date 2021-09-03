using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : MonoBehaviour
{
    public GameObject textObj;
    private TextMesh stockText;
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        stockText = textObj.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
