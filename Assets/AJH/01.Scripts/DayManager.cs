using KSM.Scripts.Planet;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("여러가지")]
    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;
    [SerializeField] MiddleResult middleResultUI;
    [SerializeField] int scenePlent;

    [Header("페이드인")]
    [SerializeField] CanvasGroup fadeCanvas;   // 필드 추가, 인스펙터에서 연결
    [SerializeField] float fadeDuration = 0.7f;

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
        dayUI.Show(_totalDay, () => timer.StartTimer(240f));
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
        StartCoroutine(ProceedAfterDayRoutine());
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        _waitingForSceneLoad = false;   // 추가
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

    bool _waitingForSceneLoad = false;

    IEnumerator ProceedAfterDayRoutine()
    {
        // 1. 페이드 아웃
        yield return Fade(0f, 1f);

        // 2. 씬 전환
        _waitingForSceneLoad = true;

        if (_currentPlanetDay >= 2)
        {
            _currentPlanetDay = 1;
            var psm = FindObjectOfType<PlanetSceneManager>();
            if (psm != null) psm.OnPlanetCleared();
        }
        else
        {
            _currentPlanetDay++;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // 3. 씬 로드가 실제로 끝날 때까지 대기
        while (_waitingForSceneLoad) yield return null;
        yield return null;   // 새 씬 초기화 한 프레임 여유

        // 4. 페이드 인
        yield return Fade(1f, 0f);
    }

    IEnumerator Fade(float from, float to)
    {
        if (fadeCanvas == null) yield break;

        fadeCanvas.gameObject.SetActive(true);
        fadeCanvas.alpha = from;

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            fadeCanvas.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeCanvas.alpha = to;

        // 완전히 밝아졌을 때만(=페이드 인 끝) 끄기
        // 페이드 아웃 끝났을 땐 검은 화면 유지해야 하므로 안 끔
        if (to <= 0f) fadeCanvas.gameObject.SetActive(false);
    }
}