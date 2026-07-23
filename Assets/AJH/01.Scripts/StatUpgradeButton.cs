using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUpgradeButton : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] private StatSO stat;
    [SerializeField] private CoinManager coinManager;

    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text costText;

    private void Start()
    {
        button.onClick.AddListener(OnClickUpgrade);
        Refresh();
    }

    private void OnEnable()
    {
        // 패널 열릴 때마다 최신 상태로
        Refresh();
    }

    private void OnClickUpgrade()
    {
        if (coinManager.TryBuyStat(stat))
        {
            Refresh();
        }
    }

    private void Refresh()
    {
        if (stat == null) return;

        nameText.text = stat.statName;
        levelText.text = $"Lv.{stat.currentLevel}";
        costText.text = $"{stat.CurrentCost} G";

        if (iconImage != null && stat.icon != null)
            iconImage.sprite = stat.icon;

        // 골드 부족하면 회색
        button.interactable = coinManager.TotalCoin >= stat.CurrentCost;
    }
}