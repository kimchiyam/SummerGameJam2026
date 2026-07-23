using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private int totalCoin = 0;
    public int TotalCoin => totalCoin;

    public void AddCoin(int amount)
    {
        if (amount <= 0) return;
        totalCoin += amount;
    }

    public bool TryBuyStat(StatSO stat)
    {
        int gold = totalCoin;
        bool success = stat.TryUpgrade(ref gold);
        if (success) totalCoin = gold;
        return success;
    }
}