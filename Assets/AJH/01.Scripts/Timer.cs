using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]float _timeLength;
    [SerializeField]float _currentTime;
    [SerializeField] TMP_Text _timeText;
    bool _isPlaying;

    public event Action OnTimerEnd;

    public void StartTimer(float time)
    {
        _isPlaying = true;
        _timeLength = time;
        _currentTime = 0f;
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Resume()
    {
        _isPlaying = true;
    }

    private void UpdateText()
    {
        if (_timeText == null) return;

        // 진행도 0~1
        float progress = _timeLength > 0 ? _currentTime / _timeLength : 0f;

        int hour = Mathf.FloorToInt(progress * 6f);
        if (hour >= 6) hour = 6;
        int displayHour = (hour == 0) ? 12 : hour;
        _timeText.text = $"{displayHour} PM";
    }

    private void Update()
    {
        // ===== 테스트용 단축키 =====
        var kb = Keyboard.current;
        if (kb != null)
        {
            // Space: 일시정지 / 재개
            if (kb.spaceKey.wasPressedThisFrame)
            {
                if (_isPlaying) Pause();
                else Resume();
            }


            // E: 즉시 하루 끝내기
            if (kb.eKey.wasPressedThisFrame)
            {
                _currentTime = _timeLength;
            }
        }

        // Shift 누르면 10배속
        float multiplier = (kb != null && kb.leftShiftKey.isPressed) ? 10f : 1f;

        if (!_isPlaying) return;
        _currentTime += Time.deltaTime * multiplier;
        if (_currentTime >= _timeLength)
        {
            _isPlaying = false;
            _currentTime = 0f;      // 미리 초기화
            UpdateText();           // 텍스트도 초기 상태(12 PM)로
            OnTimerEnd?.Invoke();
            return;
        }
        UpdateText();
    }
}