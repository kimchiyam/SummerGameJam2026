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
    
    public event Action onAilenChanged;
    public static SystemManager instance;
    public GameObject originPos;

    public bool real;
    public bool canMove = false;

    [SerializeField] private TextMeshProUGUI ageText;
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
        await Task.Delay(4000);
        canMove = true; real = true;
        FirstDialog();
        currentailen.SetActive(true);
        currentailen.gameObject.transform.DOMove(originPos.transform.position , 0.2f).OnComplete(() =>
        {
            onAilenChanged?.Invoke();
        });
        
    }
    
    public async void FirstDialog()
    {
        if(ageText == null) return;
        await Task.Delay(1000);
        ageText.gameObject.SetActive(true);
        ageText.text = $"{currentailen.GetComponent<Ailen>()._ailenSo.startScript}";
    }
}
