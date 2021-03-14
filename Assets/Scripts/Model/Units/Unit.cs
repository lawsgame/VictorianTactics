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
    [SerializeField] private bool randomLevelUp;
    [SerializeField] private Orientation initialOrientation;
    [SerializeField] private UnitType type;
    [SerializeField] private List<WeaponType> weaponTypes;

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
        Battlefield battlefieldComponent = battlefield.GetComponent<Battlefield>();
        battlefieldComponent.AddUnit(this);

        List<WeaponModel> weapons = new List<WeaponModel>();
        foreach (WeaponType weaponType in weaponTypes)
            weapons.Add(WeaponModel.create(weaponType));
        
        _groudmap = battlefieldComponent.Groundmap;
        _model = UnitModel.create(startingLevel, type, weapons, randomLevelUp, initialOrientation);
        _transform = gameObject.transform;
        _scheduler = GetComponent<UnitAnimatorManager>();
    }

    public Vector3Int GetMapPos() => _groudmap.WorldToCell(_transform.position);

    public override string ToString() => Model.ToString();
}
