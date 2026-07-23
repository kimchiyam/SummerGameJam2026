using UnityEngine;

public class TitleFloat : MonoBehaviour
{
    private float floatAmount = 10f;

    [Tooltip("움직이는 속도")]
    [SerializeField] private float floatSpeed = 2f;

    private RectTransform rectTransform;
    private Vector2 startPos;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            startPos = rectTransform.anchoredPosition;
        }
    }

    void Update()
    {
        if (rectTransform == null) return;

      
        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;

        rectTransform.anchoredPosition = new Vector2(startPos.x, newY);
    }
}
