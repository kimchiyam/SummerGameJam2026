using System;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float _timeLength;
    [SerializeField] float _currentTime;
    [SerializeField] TMP_Text _timeText;
    bool _isPlaying;

    public event Action OnTimerEnd;

    public void StartTimer(float time)
    {
        _isPlaying = true;
        _timeLength = time;
        _currentTime = 0f;
        if (_timeText != null) _timeText.gameObject.SetActive(true);
        UpdateText();
    }

    public void Pause() { _isPlaying = false; }
    public void Resume() { _isPlaying = true; }

    private void UpdateText()
    {
        if (_timeText == null) return;

        float progress = _timeLength > 0 ? _currentTime / _timeLength : 0f;

        int hour = Mathf.FloorToInt(progress * 6f);
        if (hour >= 6) hour = 6;
        int displayHour = (hour == 0) ? 12 : hour;
        _timeText.text = $"{displayHour} PM";
    }

    private void Update()
    {
        if (!_isPlaying) return;

        _currentTime += Time.deltaTime;

        if (_currentTime >= _timeLength)
        {
            _isPlaying = false;
            _currentTime = 0f;
            if (_timeText != null) _timeText.gameObject.SetActive(false);
            UpdateText();
            OnTimerEnd?.Invoke();
            return;
        }

        UpdateText();
    }

    public void ResetTimer()
    {
        _isPlaying = false;      // 타이머 정지
        _currentTime = 0f;       // 경과 시간 0으로
        if (_timeText != null)
            _timeText.gameObject.SetActive(false);  // 시간 표시 숨김
    }
}