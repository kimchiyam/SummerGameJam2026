using KSM.Scripts.Planet;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;
    [SerializeField] MiddleResult middleResultUI;
    [SerializeField] int scenePlent;

    int _currentPlanetDay = 1;
    int _totalDay = 13;

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
        timer.OnTimerEnd += EndDay;
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartNextDay();
    }


    private void OnDestroy()
    {
        if (timer != null)
            timer.OnTimerEnd -= EndDay;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PauseTime() => timer.Pause();
    public void ResumeTime() => timer.Resume();

    private void StartNextDay()
    {
        Debug.Log($"[Day] StartNextDay 진입. dayUI null? {dayUI == null}");
        dayUI.Show(_totalDay, () => timer.StartTimer(360f));
    }

    void EndDay()
    {
        _totalDay--;
        timer.Pause();

        middleResultUI.Show(() =>
        {
            CountAlien.ResetDay();
            ProceedAfterDay();
        });
    }

    void ProceedAfterDay()
    {
        if (_currentPlanetDay >= 2)
        {
            _currentPlanetDay = 1;
            var psm = FindObjectOfType<PlanetSceneManager>();
            if (psm != null)
            {
                Debug.Log("[Day] OnPlanetCleared 호출");
                psm.OnPlanetCleared();
            }
        }
        else
        {
            _currentPlanetDay++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (dayUI == null) dayUI = FindObjectOfType<DayUI>();
        if (middleResultUI == null) middleResultUI = FindObjectOfType<MiddleResult>();
        var newTimer = FindObjectOfType<Timer>();
        if (newTimer != null && newTimer != timer)
        {
            if (timer != null) timer.OnTimerEnd -= EndDay;
            timer = newTimer;
            timer.OnTimerEnd += EndDay;
        }

        if (int.TryParse(scene.name, out int planetNum) && planetNum >= 1 && planetNum <= 7)
        {
            StartNextDay();
        }
    }
}