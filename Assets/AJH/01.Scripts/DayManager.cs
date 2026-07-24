using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager instance { get; private set; }

    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;

    int _currentPlanetDay = 1;
    int _totalDay = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        timer.OnTimerEnd += EndDay;
        StartNextDay();
    }

    public void PauseTime() => timer.Pause();
    public void ResumeTime() => timer.Resume();

    private void StartNextDay()
    {
        dayUI.Show(_totalDay, () => timer.StartTimer(360f));
    }

    void EndDay()
    {
        _totalDay++;
        if (_currentPlanetDay >= 2)
            _currentPlanetDay = 1;
        else
            _currentPlanetDay++;

        StartNextDay();
    }
}