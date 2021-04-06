using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * handle area instances
 * Allowing their creation, tracking and destruction
 */
public class AreaHandler : MonoBehaviour
{
    [SerializeField] GameObject areaTemplate;

    private int cursor;
    private Dictionary<int, IAreaRenderer> areas;

    private void Awake()
    {
        areas = new Dictionary<int, IAreaRenderer>();
    }

    public int Create(Battlefield battlefield, IAreaModel model, AreaType type)
    {
        cursor += 1;
        int index = cursor;

        GameObject areaClone = GameObject.Instantiate(areaTemplate, Vector3.zero, Quaternion.identity, battlefield.transform);
        
        IAreaRenderer drawer = areaClone.GetComponent<IAreaRenderer>();
        drawer.Model = model;
        drawer.Type = type;
        drawer.Draw(battlefield.Map);

        areas.Add(index, drawer);
        return index;
    }

    public bool Remove(int index)
    {
        if (areas.ContainsKey(index))
        {
            IAreaRenderer area = areas[index];
            GameObject.Destroy(area.gameObject);
            areas.Remove(index);
            return true;
        }
        return false;
    }

    public void RemoveAll()
    {
        areas.Clear();
    }


}
