using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnitTemplate;
using static WeaponTemplate;
using UnityEngine.Tilemaps;

public class Unit : MonoBehaviour
{
    [SerializeField] private GameObject battlefield;
    [SerializeField] private int startingLevel;
    [SerializeField] private bool randomLevelUp;
    [SerializeField] private UnitType type;
    [SerializeField] private List<WeaponType> weaponTypes;
    
    private UnitModel _model;
    private Transform _transform;
    private Tilemap _groudmap;

    public UnitModel Model => _model;
    public Transform Transform => _transform;

    // Start is called before the first frame update

    private void Awake()
    {
        Battlefield battlefieldComponent = battlefield.GetComponent<Battlefield>();
        battlefieldComponent.AddUnit(this);

        List<WeaponModel> weapons = new List<WeaponModel>();
        foreach (WeaponType weaponType in weaponTypes)
            weapons.Add(WeaponModel.create(weaponType));
        
        _groudmap = battlefieldComponent.Groundmap;
        _model = UnitModel.create(startingLevel, type, weapons, randomLevelUp);
        _transform = gameObject.transform;
    }

    public Vector3Int GetMapPos() => _groudmap.WorldToCell(_transform.position);

    public override string ToString() => Model.ToString();
}
