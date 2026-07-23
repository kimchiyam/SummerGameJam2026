using System.Collections.Generic;
using UnityEngine;

public enum StatType
{
    AttackDamage,
    MoveSpeed,
    Defense,
    ProductionSpeed,
    PickupRange,
    PickupChance
}

[System.Serializable]
public class StatModifier
{
    public StatType statType;
    public float valuePerLevel;
}

[CreateAssetMenu(fileName = "State", menuName = "SO/State")]
public class UpgradeSO : ScriptableObject
{
    [Header("기본 정보")]
    public string _upgradeName;
    public Sprite _icon;
    [TextArea(2, 4)] public string description;

    [Header("레벨")]
    [Min(1)] public int _maxLevel = 5;

    [Header("스탯 증가량")]
    [Tooltip("레벨당 증가하는 스탯 목록")]
    public List<StatModifier> modifiers = new List<StatModifier>();
}