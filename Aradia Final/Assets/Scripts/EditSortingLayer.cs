using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditSortingLayer : MonoBehaviour
{
    public string SortingLayer;
    public int OrderInLayer;

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        // Set the sorting layer of the particle system.
        renderer.sortingLayerName = SortingLayer;
        renderer.sortingOrder = OrderInLayer;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
