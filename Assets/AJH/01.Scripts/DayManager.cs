using KSM.Scripts.Planet;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("��������")]
    [SerializeField] Timer timer;
    [SerializeField] DayUI dayUI;
    [SerializeField] MiddleResult middleResultUI;
    [SerializeField] int scenePlent;

    [Header("���̵���")]
    [SerializeField] CanvasGroup fadeCanvas;   // �ʵ� �߰�, �ν����Ϳ��� ����
    [SerializeField] float fadeDuration = 0.7f;

   public int _currentPlanetDay = 1;
   public int _totalDay = 13;

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
        Debug.Log($"[Day] StartNextDay ����. dayUI null? {dayUI == null}");
        dayUI.Show(_totalDay, () => timer.StartTimer(120f));
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

        _waitingForSceneLoad = false;   // �߰�
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
        // 1. ���̵� �ƿ�
        yield return Fade(0f, 1f);

        // 2. �� ��ȯ
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

        // 3. �� �ε尡 ������ ���� ������ ���
        while (_waitingForSceneLoad) yield return null;
        yield return null;   // �� �� �ʱ�ȭ �� ������ ����

        // 4. ���̵� ��
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

        // ������ ������� ����(=���̵� �� ��) ����
        // ���̵� �ƿ� ������ �� ���� ȭ�� �����ؾ� �ϹǷ� �� ��
        if (to <= 0f) fadeCanvas.gameObject.SetActive(false);
    }
}