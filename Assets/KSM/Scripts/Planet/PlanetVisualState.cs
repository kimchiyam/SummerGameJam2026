using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class PlanetVisualState : MonoBehaviour
    {
        [Header("데이터")]
        public PlanetSO planet;
 
        [Header("색상")]
        [Tooltip("도전 가능한 행성 (원래 색 그대로)")]
        public Color availableColor = Color.white;
 
        [Tooltip("이미 정복한 행성 - 채도 빠진 회색")]
        public Color clearedColor = new Color(0.45f, 0.45f, 0.5f, 1f);
 
        [Tooltip("아직 안 열린 행성 - 어둡게")]
        public Color lockedColor = new Color(0.25f, 0.25f, 0.3f, 1f);
 
        [Header("도전 가능 강조 효과")]
        [Tooltip("현재 행성을 맥박치듯 크게/작게")]
        public bool pulseWhenAvailable = true;
        public float pulseScale = 0.08f;    // 크기 변화 폭
        public float pulseSpeed = 2f;       // 맥박 속도
 
        SpriteRenderer sr;
        Vector3 baseScale;
        bool isAvailable;
 
        void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            baseScale = transform.localScale;
        }
 
        void Start()
        {
            Refresh();
        }
 
        /// <summary>
        /// 상태를 다시 읽어서 겉모습 갱신.
        /// 씬에 돌아왔을 때 자동으로 Start에서 호출됨.
        /// </summary>
        public void Refresh()
        {
            if (planet == null)
            {
                Debug.LogError(gameObject.name + " 의 planet SO가 비어있습니다.");
                return;
            }
 
            isAvailable = PlanetProgress.CanEnter(planet);
 
            if (isAvailable)
                sr.color = availableColor;
            else if (PlanetProgress.IsCleared(planet))
                sr.color = clearedColor;
            else
                sr.color = lockedColor;
 
            // 강조가 꺼지면 원래 크기로 복구
            if (!isAvailable)
                transform.localScale = baseScale;
        }
 
        void Update()
        {
            // 도전 가능한 행성만 맥박 효과
            if (!isAvailable || !pulseWhenAvailable) return;
 
            float t = 1f + Mathf.Sin(Time.time * pulseSpeed) * pulseScale;
            transform.localScale = baseScale * t;
        }
    }
}