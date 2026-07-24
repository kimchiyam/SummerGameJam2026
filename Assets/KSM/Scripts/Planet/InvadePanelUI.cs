using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace KSM.Scripts.Planet
{
    public class InvadePanelUI : MonoBehaviour
    {
       public static InvadePanelUI Instance;
 
        [Header("UI 연결")]
        public GameObject panel;
        public TMP_Text planetNameText;
        public TMP_Text descriptionText;
        public TMP_Text statusText;        // "정복 완료" / "잠김" 등 상태 표시
        public Button invadeButton;
 
        [Header("엔딩")]
        [Tooltip("엔딩 씬의 빌드 인덱스")]
        public int endingSceneIndex = 8;
 
        PlanetSO currentPlanet;
 
        void Awake()
        {
            Instance = this;
            panel.SetActive(false);
        }

        public void Show(PlanetSO planet)
        {
            currentPlanet = planet;
            panel.SetActive(true);
 
            planetNameText.text = planet.planetName;
 
            if (descriptionText != null)
                descriptionText.text = planet.planetDescription;
 
            bool canEnter = PlanetProgress.CanEnter(planet);
 
            // 상태에 따라 표시 분기
            if (PlanetProgress.IsCleared(planet))
            {
                if (statusText != null) statusText.text = "정복 완료";
                statusText.color = Color.green;
            }
            else if (PlanetProgress.IsLocked(planet))
            {
                if (statusText != null) statusText.text = "잠김 - 이전 행성을 먼저 정복하세요";
                statusText.color = Color.darkRed;
            }
            else
            {
                if (statusText != null) statusText.text = "침략 가능";
                statusText.color = Color.white;
            }
 
            if (invadeButton != null)
                invadeButton.interactable = canEnter;
        }
 
        public void Hide()
        {
            panel.SetActive(false);
            currentPlanet = null;
 
            if (PlanetCameraController.Instance != null)
                PlanetCameraController.Instance.ResetView();
        }
 
        // 침략 버튼 OnClick에 연결 (매개변수 없음)
        public void OnInvadeButton()
        {
            if (currentPlanet == null) return;
 
            if (!PlanetProgress.CanEnter(currentPlanet))
            {
                Debug.Log($"{currentPlanet.planetName}은(는) 지금 진입할 수 없습니다.");
                return;
            }
 
            PlanetSession.Current = currentPlanet;
            SceneManager.LoadScene(currentPlanet.sceneNumber);
        }
 
        void Start()
        {
            // 태양계로 돌아왔을 때 모든 행성을 깼으면 엔딩으로
            if (PlanetProgress.IsAllCleared)
                EndingSequence.Instance.PlayEnding();
        }
    }
 
    /// <summary>
    /// 씬을 넘어갈 때 "지금 들어간 행성"을 전달하는 통로
    /// </summary>
    public static class PlanetSession
    {
        public static PlanetSO Current;
    }
    }
