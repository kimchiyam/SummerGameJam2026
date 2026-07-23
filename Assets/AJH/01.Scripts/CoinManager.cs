using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [field : SerializeField] public int TotalCoin { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    
    public void AddCoin(int plus)
    {
        if (plus <= 0) return;
        TotalCoin += plus;
    }

    public void MinusCoin(int minus)
    {
        TotalCoin -= minus;
    }
}