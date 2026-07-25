using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using Button = UnityEngine.UI.Button;

public class MiddleResult : MonoBehaviour
{
    [Header("패널 / 텍스트")]
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _trueAlien;
    [SerializeField] private TextMeshProUGUI _falseAlien;
    [SerializeField] private TextMeshProUGUI _totalAlien;
 
    [Header("버튼 (인스펙터에서 드래그로 연결)")]
    [SerializeField] private Button _confirmButton;   // 확인 버튼
    // 버튼이 더 있으면 여기에 필드를 추가:
    // [SerializeField] private Button _exitButton;
 
    Action _onConfirm;
 
    private void Awake()
    {
        if (_panel != null) _panel.SetActive(false);
 
        // 코드로 버튼 연결. 인스펙터 OnClick 칸은 비워둔다.
        if (_confirmButton != null)
        {
            _confirmButton.onClick.RemoveAllListeners();  // 중복 방지
            _confirmButton.onClick.AddListener(OnConfirmClicked);
        }
 
        // 버튼이 더 있으면 같은 방식으로:
        // if (_exitButton != null)
        // {
        //     _exitButton.onClick.RemoveAllListeners();
        //     _exitButton.onClick.AddListener(OnExitClicked);
        // }
    }
 
    public void Show(Action onConfirm)
    {
        _onConfirm = onConfirm;
 
        _trueAlien.text = $"진짜 외계인: {CountAlien.TrueAlienCount}";
        _falseAlien.text = $"가짜 외계인: {CountAlien.FalseAlienCount}";
        _totalAlien.text = $"전체 외계인 수: {CountAlien.AlienCount}";
 
        _panel.SetActive(true);
    }
 
    public void OnConfirmClicked()
    {
        _panel.SetActive(false);
        var cb = _onConfirm;
        _onConfirm = null;
        cb?.Invoke();
    }
 
    // 버튼이 더 있으면 함수도 추가:
    // public void OnExitClicked()
    // {
    //     _panel.SetActive(false);
    //     // 홈으로 가기 등의 동작
    // }
}
