using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Stat", menuName = "SO/StatSO")]
public class StatSO : ScriptableObject
{
    [Header("기본 정보")]
    public string statName;
    public Sprite icon;
    [TextArea(2, 4)] public string description;

    [Header("스탯 값")]
    public float baseValue = 10f;
    public float valuePerLevel = 2f;

    [Header("레벨업 비용")]
    public int baseCost = 100;
    public float costMultiplier = 1.5f;

    [Header("런타임 상태")]
    public int currentLevel = 0;

    [System.NonSerialized] public UnityEvent onLevelUp = new UnityEvent();

    public float CurrentValue => baseValue + valuePerLevel * currentLevel;

    public int CurrentCost => Mathf.RoundToInt(baseCost * Mathf.Pow(costMultiplier, currentLevel));

    public bool TryUpgrade(ref int gold)
    {
        int cost = CurrentCost;
        if (gold < cost) return false;

        gold -= cost;
        currentLevel++;
        onLevelUp?.Invoke();
        return true;
    }
}
