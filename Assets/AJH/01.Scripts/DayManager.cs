using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;
    [SerializeField] int scenePlent;

    int _currentPlanetDay = 1;
    int _totalDay = 1;

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
        dayUI.Show(_totalDay, () => timer.StartTimer(360f));
    }

    void EndDay()
    {
        _totalDay++;

        if (_currentPlanetDay >= 2)
        {
            _currentPlanetDay = 1;
            SceneManager.LoadScene(scenePlent);
        }
        else
        {
            _currentPlanetDay++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            StartNextDay();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex <= 6) 
        {
            StartNextDay();
        }
    }
}