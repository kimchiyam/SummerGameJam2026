using UnityEngine;
using UnityEngine.SceneManagement;

namespace KSM.Scripts.Planet
{
    public class PlanetSceneManager : MonoBehaviour
    {
        [Header("설정")]
        public int solarSystemSceneIndex = 0;
        public int endingSceneIndex = 8;
        public float returnDelay = 2f;
 
        PlanetSO thisPlanet;
 
        void Start()
        {
            thisPlanet = PlanetSession.Current;
 
            if (thisPlanet == null)
            {
                Debug.LogWarning("PlanetSession.Current가 비어있습니다. 태양계 씬에서 진입하세요.");
                return;
            }
 
            Debug.Log($"{thisPlanet.planetName} 진입");
        }
 
        /// <summary>
        /// ★ 행성을 깼을 때 호출 ★
        /// </summary>
        public void OnPlanetCleared()
        {
            if (thisPlanet == null)
            {
                Debug.LogError("클리어 처리 실패: 행성 정보가 없습니다.");
                return;
            }
 
            // 진행도 갱신 (다음 행성이 열림)
            PlanetProgress.ClearPlanet(thisPlanet);
 
            // 마지막 행성이었으면 엔딩으로, 아니면 태양계로
            if (PlanetProgress.IsAllCleared)
                Invoke(nameof(GoToEnding), returnDelay);
            else
                Invoke(nameof(ReturnToSolarSystem), returnDelay);
        }
 
        /// <summary>클리어 실패 시 (재도전 가능)</summary>
        public void OnPlanetFailed()
        {
            Invoke(nameof(ReturnToSolarSystem), returnDelay);
        }
 
        void ReturnToSolarSystem() => SceneManager.LoadScene(solarSystemSceneIndex);
        void GoToEnding() => SceneManager.LoadScene(endingSceneIndex);
    }
    
}