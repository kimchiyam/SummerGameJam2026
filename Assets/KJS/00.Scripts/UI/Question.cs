using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private int index;
    private Image _srr;
    private bool canAnswer = true;

    private void Start()
    {
        _srr = GetComponent<Image>();
    }

    public void TakeQustion()
    {   
        if (SystemManager.instance.canMove)
        {
            if (canAnswer)
            {
                text.text = $"{SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.scripts[index]}";
                _srr.color = new Vector4(1f, 1f, 1f, 0.3f);
                canAnswer = false;
            }
            else
            {
                text.text = $"이미 말했잖아요.";
            }

            text.rectTransform.DOAnchorPos(new Vector3(text.rectTransform.anchoredPosition.x, text.rectTransform.anchoredPosition.y + 10, 0), 0.1f).OnComplete(() =>
            {
                text.rectTransform.DOAnchorPos(new Vector3(text.rectTransform.anchoredPosition.x ,  text.rectTransform.anchoredPosition.y - 10, 0) , 0.1f);
            });
            
            
        }
        else
        {   
            Debug.Log("넘어갈수 없습니다.");
        }
        
    }
}
