using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Battlefield : MonoBehaviour
{
    public GameObject tilePosIndicator;

    private Tilemap map;
    private PartyRelationshipRecorder partyRecorder;
    private BattlefieldDecorator decorator;
    private UnitRegistry unitRegistry;

    public Tilemap Map => map;
    public List<Unit> Units() => unitRegistry.Units(true);
    public BattlefieldDecorator Decorator => decorator;
    public UnitRegistry UnitRegistry => unitRegistry;

    private void Awake()
    {
        map = GetComponent<Tilemap>();
        partyRecorder = GetComponent<PartyRelationshipRecorder>();
        decorator = GetComponent<BattlefieldDecorator>();
        unitRegistry = GetComponent<UnitRegistry>();
    }

    private void Start()
    {
        decorator.Decorate();
    }

    public void SetTile(WorldTile worldTile, Vector3Int tilePos)
    {
        map.SetTile(tilePos, worldTile);
        decorator.UpdateDecorationIfAny(tilePos, worldTile);
    }

    public bool IsTileOccupied(Vector3Int cellpos) => GetUnitFrom(cellpos) != null;

    public bool IsTileOccupiedByAlly(Vector3Int cellpos, int partyNumber)
    {
        Unit unit = GetUnitFrom(cellpos);
        return unit != null && partyRecorder.SameSide(unit.Model.Party(), partyNumber);
    }

    public bool IsTileOccupiedByFoe(Vector3Int cellpos, int partyNumber)
    {
        Unit unit = GetUnitFrom(cellpos);
        return unit != null && partyRecorder.AreOpposed(unit.Model.Party(), partyNumber);
    }


    public Unit GetUnitFrom(Vector3Int mapPos)
    {
        Unit found = null;
        Vector3Int unitPos;
        foreach (Unit u in Units())
        {
            if (u.gameObject.activeInHierarchy)
            {
                unitPos = map.WorldToCell(u.Transform.position);
                if (unitPos.Equals(mapPos))
                {
                    found = u;
                    break;
                }
            }
        }
        return found;
    }
}
