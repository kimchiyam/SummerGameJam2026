using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private CoinManager coinManager;

    [Header("공용 UI")]
    [SerializeField] private TMP_Text coinText;

    [Header("패널")]
    [SerializeField] private GameObject statPanel;

    private GameObject currentPanel;
    private int lastCoin = -1;

    private void Start()
    {
        // 시작 시 모든 패널 닫기
        if (statPanel != null) statPanel.SetActive(false);
    }

    private void Update()
    {
        // 코인 표시 갱신
        int current = coinManager.TotalCoin;
        if (current != lastCoin)
        {
            lastCoin = current;
            coinText.text = $"{current} G";
        }

        // ESC로 창 닫기
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            ClosePanel();
        }
    }

    // 스탯창 열기 (버튼 OnClick에 연결)
    public void OpenStatPanel()
    {
        OpenPanel(statPanel);
    }

    // 닫기 버튼에 연결
    public void ClosePanel()
    {
        if (currentPanel == null) return;
        currentPanel.SetActive(false);
        currentPanel = null;
    }

    private void OpenPanel(GameObject panel)
    {
        if (panel == null) return;
        if (currentPanel != null) currentPanel.SetActive(false);
        currentPanel = panel;
        panel.SetActive(true);
    }
}