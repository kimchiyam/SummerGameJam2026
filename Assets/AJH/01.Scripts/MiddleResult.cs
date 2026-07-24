using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class MiddleResult : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _trueAlien;
    [SerializeField] private TextMeshProUGUI _falseAlien;
    [SerializeField] private TextMeshProUGUI _totalAlien;

    Action _onConfirm;

    private void Awake()
    {
        if (_panel != null) _panel.SetActive(false);
    }

    public void Show(Action onConfirm)
    {
        _onConfirm = onConfirm;

        _trueAlien.text = $"진짜 외계인: {CountAlien.TrueAlienCount}";
        _falseAlien.text = $"가짜 외계인: {CountAlien.FalseAlienCount}";
        _totalAlien.text = $"총 외계인 수: {CountAlien.AlienCount}";

        _panel.SetActive(true);
    }

    public void OnConfirmClicked()
    {
        _panel.SetActive(false);
        var cb = _onConfirm;
        _onConfirm = null;
        cb?.Invoke();
    }
}
