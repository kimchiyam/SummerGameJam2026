using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SystemManager : MonoBehaviour
{
    public GameObject[] ailens;
    public GameObject currentailen;

    public event Action OnAilenChanged;

    public static SystemManager instance;

    [Header("Spawn")]
    [SerializeField] private Transform spawnPos;
    [SerializeField] private Transform originPos;
    [SerializeField] private Transform passExitPos;
    [SerializeField] private Transform outExitPos;

    public bool real;
    public bool canMove = false;

    [SerializeField] private RectTransform id;
    [SerializeField] private RectTransform idOriginPos;
    [SerializeField] private RectTransform idStartPos;

    public TextMeshProUGUI ageText;

    // 이미 등장한 외계인
    private readonly HashSet<int> spawnedIndexes = new();

    // 마지막에 등장한 외계인
    private int lastSpawnIndex = -1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ageText.gameObject.SetActive(false);

        spawnedIndexes.Clear();
        lastSpawnIndex = -1;

        SpawnAilen();
    }

    /// <summary>
    /// 외계인 생성
    /// </summary>
    public void SpawnAilen()
    {
        canMove = false;

        ageText.gameObject.SetActive(false);

        id.anchoredPosition = idStartPos.anchoredPosition;

        int randomIndex;

        // 아직 등장하지 않은 외계인
        List<int> newIndexes = new();

        for (int i = 0; i < ailens.Length; i++)
        {
            if (!spawnedIndexes.Contains(i))
                newIndexes.Add(i);
        }

        // 아직 안 나온 외계인이 있다면
        if (newIndexes.Count > 0)
        {
            randomIndex = newIndexes[Random.Range(0, newIndexes.Count)];

            spawnedIndexes.Add(randomIndex);
        }
        else
        {
            // 모두 한 번씩 나왔으면
            // 마지막 외계인만 제외
            List<int> candidates = new();

            for (int i = 0; i < ailens.Length; i++)
            {
                if (i != lastSpawnIndex)
                    candidates.Add(i);
            }

            randomIndex = candidates[Random.Range(0, candidates.Count)];
        }

        lastSpawnIndex = randomIndex;

        currentailen = Instantiate(
            ailens[randomIndex],
            spawnPos.position,
            Quaternion.identity);

        currentailen.SetActive(false);

        PopAilen();
    }

    /// <summary>
    /// 외계인 등장
    /// </summary>
    private async void PopAilen()
    {
        await Task.Delay(3500);

        real = currentailen.GetComponent<Ailen>()._ailenSo.isReal;

        currentailen.SetActive(true);

        currentailen.transform
            .DOMove(originPos.position, 0.2f)
            .OnComplete(() =>
            {
                OnAilenChanged?.Invoke();
            });

        FirstDialog();

        await Task.Delay(2000);

        id.DOAnchorPos(idOriginPos.anchoredPosition, 0.4f)
            .OnComplete(() =>
            {
                // 여권이 완전히 내려온 뒤에만 검문 가능
                canMove = true;
            });
    }

    /// <summary>
    /// 외계인 퇴장
    /// </summary>
    public void ExitAilen(bool isPass)
    {
        canMove = false;

        ageText.gameObject.SetActive(false);

        Vector3 targetPos = isPass
            ? passExitPos.position
            : outExitPos.position;

        id.DOAnchorPos(idStartPos.anchoredPosition, 0.4f);

        currentailen.transform
            .DOMove(targetPos, 0.5f)
            .OnComplete(() =>
            {
                Destroy(currentailen);

                SpawnAilen();
            });
    }

    private async void FirstDialog()
    {
        if (ageText == null)
            return;

        await Task.Delay(1000);

        ageText.gameObject.SetActive(true);
        ageText.text = currentailen.GetComponent<Ailen>()._ailenSo.startScript;
    }
}