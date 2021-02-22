using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

    private Battlefield battlefieldComponent;

    // Start is called before the first frame update

    private void Awake()
    {
        GameObject battlefieldObject = GameObject.FindGameObjectWithTag("BattleField");
        battlefieldComponent = battlefieldObject.GetComponent<Battlefield>();
        battlefieldComponent.AddUnit(this);
    }

}
