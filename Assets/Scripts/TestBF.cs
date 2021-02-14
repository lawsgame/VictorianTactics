using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestBF : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Tilemap ground;

    void Start()
    {
        Debug.Log("Test BF");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("begin calculation...");
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int clickedCell = ground.WorldToCell(mousePosition);
            Debug.Log("cell clicked on: " + clickedCell);
        }
    }

}
