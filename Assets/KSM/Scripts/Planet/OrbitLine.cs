using UnityEngine;

namespace KSM.Scripts.Planet
{
    [RequireComponent(typeof(LineRenderer))]
    public class OrbitLine : MonoBehaviour
    {
        [Header("궤도 설정")] public Transform sun; // 태양 (중심점)
        public float radius = 3f; // 궤도 반지름 = 행성이 태양에서 떨어진 거리
        public int segments = 100; // 원을 이루는 점 개수. 많을수록 매끄러움

        [Header("선 스타일")] public float lineWidth = 0.05f;
        public Color lineColor = new Color(1f, 1f, 1f, 0.3f); // 반투명 흰색

        void Start()
        {
            DrawCircle();
        }

        void DrawCircle()
        {
            LineRenderer lr = GetComponent<LineRenderer>();

            lr.positionCount = segments + 1; // 마지막 점 = 첫 점 (원을 닫음)
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.useWorldSpace = true;
            lr.loop = true;

            // 기본 머티리얼 + 색상 설정
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = lineColor;
            lr.endColor = lineColor;

            Vector3 center = sun != null ? sun.position : Vector3.zero;

            for (int i = 0; i <= segments; i++)
            {
                float angle = (float)i / segments * 2f * Mathf.PI; // 0 ~ 360도를 라디안으로
                float x = center.x + Mathf.Cos(angle) * radius;
                float y = center.y + Mathf.Sin(angle) * radius;
                lr.SetPosition(i, new Vector3(x, y, 0f));
            }
        }
    }
}