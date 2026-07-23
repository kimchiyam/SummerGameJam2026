using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class OrbitLineManager : MonoBehaviour
    {
        [Header("기본 설정")]
        public Transform sun;             // 태양 드래그
        public int orbitCount = 8;        // 궤도 개수
        public float firstRadius = 1.5f;  // 첫 번째(가장 안쪽) 궤도 반지름
        public float gap = 0.8f;          // 궤도 사이 간격
 
        [Header("선 스타일")]
        public float lineWidth = 0.04f;
        public int segments = 100;
        public Color[] colors = new Color[]  // 궤도마다 번갈아 쓸 색
        {
            new Color(0.4f, 0.8f, 0.8f, 0.5f), // 청록
            new Color(0.6f, 0.85f, 0.4f, 0.5f), // 연두
            new Color(0.4f, 0.6f, 0.95f, 0.5f), // 파랑
        };
 
        void Start()
        {
            Vector3 center = sun != null ? sun.position : Vector3.zero;
 
            for (int i = 0; i < orbitCount; i++)
            {
                float radius = firstRadius + gap * i;
                CreateOrbit(center, radius, colors[i % colors.Length], i);
            }
        }
 
        void CreateOrbit(Vector3 center, float radius, Color color, int index)
        {
            // 궤도용 자식 오브젝트 생성
            GameObject orbitObj = new GameObject("Orbit_" + (index + 1));
            orbitObj.transform.parent = transform;
 
            LineRenderer lr = orbitObj.AddComponent<LineRenderer>();
            lr.positionCount = segments + 1;
            lr.startWidth = lineWidth;
            lr.endWidth = lineWidth;
            lr.useWorldSpace = true;
            lr.loop = true;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = color;
            lr.endColor = color;
            lr.sortingOrder = -1; // 행성보다 뒤에 그리기
 
            for (int i = 0; i <= segments; i++)
            {
                float angle = (float)i / segments * 2f * Mathf.PI;
                float x = center.x + Mathf.Cos(angle) * radius;
                float y = center.y + Mathf.Sin(angle) * radius;
                lr.SetPosition(i, new Vector3(x, y, 0f));
            }
        }
    }
}