using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitRegistry : MonoBehaviour
{

    private List<Unit> units;

    private void Awake()
    {
        units = new List<Unit>();
    }

    public List<Unit> ActiveUnits() => Units(true);

    public void AddUnit(Unit unit)
    {
        if (units == null)
            units = new List<Unit>();
        units.Add(unit);
    }

    public List<Unit> Units(bool activeOnly)
    {
        List<Unit> activeUnits = new List<Unit>();
        foreach (Unit u in units)
        {
            if (u.gameObject.activeInHierarchy || !activeOnly)
            {
                activeUnits.Add(u);
            }
        }
        return activeUnits;
    }

    
}
