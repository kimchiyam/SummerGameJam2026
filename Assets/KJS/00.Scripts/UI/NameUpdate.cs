using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NameUpdate : MonoBehaviour
{
    private TextMeshProUGUI nameText;
    private void Start()
    {
        nameText = GetComponent<TextMeshProUGUI>();
        SystemManager.instance.OnAilenChanged += NameChange;
    }

    public void NameChange()
    {
        nameText.text = $"이름 : {SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.ailenName}";
    }
    
}
