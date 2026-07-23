using System;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class SystemManager : MonoBehaviour
{
    [SerializeField] GameObject[] ailens;
    private int randomIndex;
    
    private event Action onAilenChanged;
    public static SystemManager instance;

    private void Awake()
    {
        instance = this;
        
    }

    private void Start()
    {
        randomIndex = Random.Range(0, ailens.Length);
        PopAilen(randomIndex);
    }

    private async void PopAilen(int index)
    {
        await Task.Delay(4000);
        Instantiate(ailens[index], transform);
        ailens[index].SetActive(true);
        Debug.Log("PopAilen");
        onAilenChanged?.Invoke();
    }
}
