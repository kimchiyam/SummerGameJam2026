using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Timer : MonoBehaviour
{
    [SerializeField]float _timeLength;
    [SerializeField]float _currentTime;
    bool _isPlaying;

    public event Action OnTimerEnd;

    public void StartTimer(float time)
    {
        _isPlaying = true;
        _timeLength = time;
        _currentTime = 0f;
        Debug.Log($"[Timer] StartTimer - {time}초 타이머 시작");
    }

    public void Pause()
    {
        _isPlaying = false;
        Debug.Log($"[Timer] Pause - {_currentTime:F2}초에서 정지");
    }

    public void Resume()
    {
        _isPlaying = true;
        Debug.Log($"[Timer] Resume - {_currentTime:F2}초에서 재개");
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
                Debug.Log("[Timer] 테스트: 강제 종료");
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
            Debug.Log($"[Timer] 타이머 종료! ({_timeLength}초 경과)");
            OnTimerEnd?.Invoke();
        }
    }
}