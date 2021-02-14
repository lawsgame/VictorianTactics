using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInputManager : MonoBehaviour
{
    private GameObject battlefield;

    // Start is called before the first frame update
    void Start()
    {
        battlefield = GameObject.FindGameObjectWithTag("Battlefield");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        
    }
}
