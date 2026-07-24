using DG.Tweening;
using TMPro;
using UnityEngine;

public class DayUI : MonoBehaviour
{
    [SerializeField] private RectTransform _panel;
    [SerializeField] private TextMeshProUGUI _dayText;
    [SerializeField] float _hidePosY = 100f;
    [SerializeField] float _showPosY = 0f;
    [SerializeField] float _slideInDuration = 0.5f;
    [SerializeField] float _stayDuration = 2f;
    [SerializeField] float _fadeDuration = 0.3f;

    RectTransform _rect;

    private void Awake()
    {
        _rect = _dayText.rectTransform;
    }

    public void Show(int Day, System.Action onComplete = null)
    {
        _panel.gameObject.SetActive(true);

        _dayText.text = $"{Day}일차";
        _rect.anchoredPosition = new Vector2(0, _hidePosY);
        _dayText.alpha = 1f;

        Sequence seq = DOTween.Sequence();
        seq.Append(_rect.DOAnchorPosY(_showPosY, _slideInDuration).SetEase(Ease.OutBack));
        seq.AppendInterval(_stayDuration);
        seq.Append(_dayText.DOFade(0f, _fadeDuration));
        seq.OnComplete(() => {
            _panel.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
}