using DG.Tweening;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dayUI;
    [SerializeField] float _hidePosY = 300f;
    [SerializeField] float _showPosY =0f;
    
    RectTransform _rect;

    private void Awake()
    {
        _rect = _dayUI.rectTransform;
    }

    public void Show(int Day)
    {
        _dayUI.text = $"{Day}일차";
        _rect.anchoredPosition = new Vector2(0,_hidePosY);

        Sequence seq = DOTween.Sequence();
        seq.Append(_rect.DOAnchorPosY(_showPosY, 0.5f).SetEase(Ease.OutBack));
        seq.AppendInterval(2f);
        seq.Append(_rect.DOAnchorPosY(_hidePosY, 0.5f).SetEase(Ease.InBack));
    }
}
