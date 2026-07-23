using UnityEngine;
using TMPro;
namespace KSM.Scripts.Planet
{
    public class InvadePanelUI : MonoBehaviour
    {
        public static InvadePanelUI Instance; // 어디서든 접근하기 쉽게 싱글톤
 
        [Header("UI 연결")]
        public GameObject panel;           // 오른쪽 네모 패널 오브젝트
        public TMP_Text planetNameText;    // "지구 침략하기" 같은 텍스트
 
        string currentPlanet;
 
        void Awake()
        {
            Instance = this;
            panel.SetActive(false); // 시작할 땐 숨김
        }
 
        // 행성을 클릭하면 호출됨
        public void Show(string planetName)
        {
            currentPlanet = planetName;
            planetNameText.text = planetName + " 침략하기";
            panel.SetActive(true);
        }
 
        // 패널의 X(닫기) 버튼 OnClick에 연결
        public void Hide()
        {
            panel.SetActive(false);
        }
 
        // "침략하기" 버튼 OnClick에 연결
        public void OnInvadeButton()
        {
            Debug.Log(currentPlanet + " 침략 시작!");
            // TODO: 여기서 침략 씬 로드 or 침략 로직 실행
            // 예: UnityEngine.SceneManagement.SceneManager.LoadScene("InvadeScene");
        }
    }
}