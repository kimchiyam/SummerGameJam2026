using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    Timer timer;
    int _currentPlanetDay = 1;
    int _totalDay = 1;

    public event Action OnEndDay;
    public event Action OnStartDay;

    private void Start()
    {
        timer.OnTimerEnd += EndDay;
        timer.StartTimer(360f);
    }

    void EndDay()
    {
        _totalDay++;
        if(_currentPlanetDay >= 2)
        {
            _currentPlanetDay = 1;
        }
        else
        {
            _currentPlanetDay++;
            timer.StartTimer(360);
        }
    }
}
