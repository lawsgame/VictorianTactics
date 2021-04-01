using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Data;

[CreateAssetMenu(fileName = "New Weapon Template", menuName = "Weapon")]
public class WeaponTemplate : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private WeaponClass clazz;
    [SerializeField] private WeaponSubClass subClazz;
    [SerializeField] private int rangeMin;
    [SerializeField] private int rangeMax;
    [SerializeField] private int powerMin;
    [SerializeField] private int powerMax;
    [SerializeField] private DamageType damageType;
    [SerializeField] private int weight;
    [SerializeField] private int accuracy;
    [SerializeField] private float dropRate;
    [SerializeField] private int durabilityMax;

    public string Name() => _name;
    public WeaponClass Class() => clazz;
    public WeaponSubClass SubClass() => subClazz;
    public int RangeMin() => rangeMin;
    public int RangeMax() => rangeMax;
    public int PowerMin() => powerMin;
    public int PowerMax() => powerMax;
    public DamageType GetDamageType() => damageType;
    public int Weight() => weight;
    public int Accuracy() => accuracy;
    public float DropRate() => dropRate;
    public int DurabilityMax() => durabilityMax;
}
