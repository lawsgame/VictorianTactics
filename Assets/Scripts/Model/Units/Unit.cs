using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnitTemplate;
using static WeaponTemplate;
using UnityEngine.Tilemaps;
using static Data;

public class Unit : MonoBehaviour
{
    // initialization parameters
    [SerializeField] private GameObject battlefield;
    [SerializeField] private int startingLevel;
    [SerializeField] private int party;
    [SerializeField] private bool randomLevelUp;
    [SerializeField] private Orientation initialOrientation;
    [SerializeField] private UnitTemplate type;
    [SerializeField] private List<WeaponTemplate> weaponTypes;

    private UnitModel _model;
    private Transform _transform;
    private Tilemap _groudmap;
    private UnitAnimatorManager _scheduler;

    public UnitModel Model => _model;
    public Transform Transform => _transform;
    public UnitAnimatorManager Scheduler => _scheduler;
    public Orientation InitialOrientation => initialOrientation;

    // Start is called before the first frame update

    private void Awake()
    {
        Battle battlefieldComponent = battlefield.GetComponent<Battle>();
        battlefieldComponent.AddUnit(this);

        List<WeaponModel> weapons = new List<WeaponModel>();
        foreach (WeaponTemplate weaponType in weaponTypes)
            weapons.Add(WeaponModel.create(weaponType));
        
        _groudmap = battlefieldComponent.Battlefield;
        _model = UnitModel.create(startingLevel, party, type, weapons, randomLevelUp, initialOrientation);
        _transform = gameObject.transform;
        _scheduler = GetComponent<UnitAnimatorManager>();
    }

    public Vector3Int GetMapPos() => _groudmap.WorldToCell(_transform.position);

    public override string ToString() => Model.ToString();
}
