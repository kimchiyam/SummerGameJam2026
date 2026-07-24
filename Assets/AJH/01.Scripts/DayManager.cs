using System;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;

    int _currentPlanetDay = 1;
    int _totalDay = 1;
    [SerializeField]float _currentTime = 360f;

    public event Action OnEndDay;
    public event Action OnStartDay;

    public static DayManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Debug.Log("[DayManager] Start - 게임 시작");
        timer.OnTimerEnd += EndDay;
        StartNextDay();
    }

    private void StartNextDay()
    {
        Debug.Log($"[DayManager] StartNextDay - 총 {_totalDay}일차 / 행성 내 {_currentPlanetDay}일차");
        dayUI.Show(_totalDay);
        timer.StartTimer(360f);
    }

    void EndDay()
    {
        Debug.Log($"[DayManager] EndDay - {_totalDay}일차 종료");

        _totalDay++;

        if (_currentPlanetDay >= 2)
        {
            _currentPlanetDay = 1;
            Debug.Log("[DayManager] 행성 나갈 시간! 다음 행성 1일차로 리셋");
        }
        else
        {
            _currentPlanetDay++;
            Debug.Log($"[DayManager] 같은 행성 계속 - 행성 내 {_currentPlanetDay}일차로");
        }

        StartNextDay();
    }
}