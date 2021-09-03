using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShelfController : MonoBehaviour
{
    public GameObject textObj;
    private TextMeshPro stockText;
    private StockContainer container;

    // Start is called before the first frame update
    void Start()
    {
        container = gameObject.GetComponent<StockContainer>();
        stockText = textObj.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        stockText.text = container.Stock + "/" + container.MaxStock;
    }
}
