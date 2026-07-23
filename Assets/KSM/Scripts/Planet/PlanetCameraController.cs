using UnityEngine;
using Unity.Cinemachine;
namespace KSM.Scripts.Planet
{
    public class PlanetCameraController : MonoBehaviour
    {
        public static PlanetCameraController Instance;
 
        [Header("시네머신")]
        public CinemachineCamera vcam;   // 3.x: CinemachineCamera
        public Transform sun;            // 태양 (기본 시점)
 
        [Header("줌 설정")]
        public float zoomInSize = 8f;    // 행성 따라갈 때 화면 크기 (작을수록 확대)
        public float defaultSize = 35f;  // 전체 태양계 보는 크기
        public float zoomSpeed = 2f;     // 줌 부드러움
 
        float targetSize;
 
        void Awake()
        {
            Instance = this;
            targetSize = defaultSize;
        }
 
        void Start()
        {
            vcam.Follow = sun;
            vcam.Lens.OrthographicSize = defaultSize;   // 3.x: m_Lens → Lens
        }
 
        void Update()
        {
            // 목표 줌 값으로 부드럽게 이동
            var lens = vcam.Lens;
            lens.OrthographicSize = Mathf.Lerp(
                lens.OrthographicSize,
                targetSize,
                Time.deltaTime * zoomSpeed
            );
            vcam.Lens = lens;   // 구조체라서 다시 넣어줘야 적용됨
        }
 
        // 행성 클릭 시 호출
        public void FocusOn(Transform planet)
        {
            vcam.Follow = planet;
            targetSize = zoomInSize;
        }
 
        // 전체 뷰로 복귀
        public void ResetView()
        {
            vcam.Follow = sun;
            targetSize = defaultSize;
        }
    }
}