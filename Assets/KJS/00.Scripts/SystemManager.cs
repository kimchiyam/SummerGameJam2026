using System;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SystemManager : MonoBehaviour
{
    public GameObject[] ailens;
    public GameObject currentailen;
    private int randomIndex;
    
    public event Action OnAilenChanged;
    public static SystemManager instance;
    public GameObject originPos;

    public bool real;
    public bool canMove = false;
    [SerializeField] private RectTransform id;
    [SerializeField] private RectTransform idOriginPos;

    public TextMeshProUGUI ageText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ageText.gameObject.SetActive(false);
        randomIndex = Random.Range(0, ailens.Length);
        currentailen = Instantiate(ailens[randomIndex], transform.position , Quaternion.identity);
        currentailen.SetActive(false);
        PopAilen(randomIndex);
    }

    private async void PopAilen(int index)
    {
        await Task.Delay(3500);
        canMove = true;
        real = currentailen.GetComponent<Ailen>()._ailenSo.isReal;
        FirstDialog();
        currentailen.SetActive(true);
        currentailen.gameObject.transform.DOMove(originPos.transform.position , 0.2f).OnComplete(() =>
        {
            OnAilenChanged?.Invoke();
        });
        await Task.Delay(2000);
        id.DOAnchorPos(idOriginPos.anchoredPosition, 0.4f);

    }
    
    public async void FirstDialog()
    {
        if(ageText == null) return;
        await Task.Delay(1000);
        ageText.gameObject.SetActive(true);
        ageText.text = $"{currentailen.GetComponent<Ailen>()._ailenSo.startScript}";
    }
}
