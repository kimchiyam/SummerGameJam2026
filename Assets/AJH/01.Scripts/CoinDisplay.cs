using UnityEngine;
using TMPro;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private CoinManager coinManager;

    private int lastCoin = -1;


    private void Update()
    {
        // 값 바뀌었을 때만 텍스트 갱신 (매 프레임 문자열 생성 방지)
        if (coinManager.TotalCoin != lastCoin)
        {
            lastCoin = coinManager.TotalCoin;
            coinText.text = $"{lastCoin} G";
        }
    }
}