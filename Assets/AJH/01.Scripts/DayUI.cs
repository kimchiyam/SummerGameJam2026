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
    private CanvasGroup _panelGroup;

    RectTransform _rect;

    private void Awake()
    {
        _rect = _dayText.rectTransform;
        _panelGroup = _panel.GetComponent<CanvasGroup>();
    }

    public void Show(int Day, System.Action onComplete = null)
    {
        _panel.gameObject.SetActive(true);
        _panelGroup.alpha = 1f;
        _dayText.text = Day==0? "D-Day":$"{Day}-Day";
        _rect.anchoredPosition = new Vector2(0, _hidePosY);


        Sequence seq = DOTween.Sequence();
        seq.SetUpdate(true);
        seq.Append(_rect.DOAnchorPosY(_showPosY, _slideInDuration).SetEase(Ease.OutBack));
        seq.AppendInterval(_stayDuration);
        seq.Append(_panelGroup.DOFade(0f, _fadeDuration));

        seq.OnComplete(() =>
        {
            _panel.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }
}