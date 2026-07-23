using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrate", menuName = "Scriptable Objects/Upgrate")]
public class Upgrate : ScriptableObject
{

}

public enum State
{
    AttackDamage,
    Speed,
    Defance,
    Intelligence,

    QicklyEarn,
    PickUp
}

[System.Serializable]
public class StatModifier
{
    public State statType;
    public float valuePerLevel;
}

[CreateAssetMenu(fileName = "UpGrade", menuName = "SO/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    [Header("기본 정보")]
    public string upgradeName;
    public Sprite icon;
    [TextArea(2, 4)] public string description;

    [Header("레벨")]
    [Min(1)] public int maxLevel = 5;

    [Header("스탯 증가량")]
    [Tooltip("레벨당 증가하는 스탯 목록")]
    public List<StatModifier> modifiers = new List<StatModifier>();
}