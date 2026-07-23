using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    float _timeLength;
    float _currentTime;

    bool _isPlaying;

    public event Action OnTimerEnd;

    public void StartTimer(float time)
    {
        _isPlaying = true;
        _timeLength = time;
    }

    public void Pause()
    {
        _isPlaying = false;
    }

    public void Resume()
    {
        _isPlaying = true;
    }

    private void Update()
    {
        if (!_isPlaying) return;
        _currentTime += Time.deltaTime;
        if(_currentTime >= _timeLength)
        {
            _isPlaying = false;
            OnTimerEnd?.Invoke();
        }
    }
}