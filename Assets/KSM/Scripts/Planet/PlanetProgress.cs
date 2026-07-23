using System.Collections.Generic;
using UnityEngine;

namespace KSM.Scripts.Planet
{
    public static class PlanetProgress
    {
        /// <summary>지금 도전 가능한 행성의 인덱스</summary>
        public static int CurrentIndex { get; private set; } = 0;
 
        /// <summary>전체 행성 개수. 이 수만큼 깨면 엔딩.</summary>
        public const int TOTAL_PLANETS = 7;
 
        /// <summary>모든 행성을 정복했는가? (엔딩 조건)</summary>
        public static bool IsAllCleared => CurrentIndex >= TOTAL_PLANETS;
 
        /// <summary>
        /// 이 행성에 지금 진입할 수 있는가?
        /// 딱 현재 차례인 행성만 true
        /// </summary>
        public static bool CanEnter(PlanetSO planet)
        {
            if (planet == null) return false;
            return planet.planetIndex == CurrentIndex;
        }
 
        /// <summary>
        /// 이미 깬 행성인가? (현재보다 앞 순서면 클리어한 것)
        /// </summary>
        public static bool IsCleared(PlanetSO planet)
        {
            if (planet == null) return false;
            return planet.planetIndex < CurrentIndex;
        }
 
        /// <summary>
        /// 아직 순서가 안 온 행성인가? (잠금 상태)
        /// </summary>
        public static bool IsLocked(PlanetSO planet)
        {
            if (planet == null) return true;
            return planet.planetIndex > CurrentIndex;
        }
 
        /// <summary>
        /// 행성 클리어! 다음 행성이 열린다.
        /// </summary>
        public static void ClearPlanet(PlanetSO planet)
        {
            if (planet == null) return;
 
            // 현재 차례인 행성만 클리어 처리 (순서 꼬임 방지)
            if (planet.planetIndex != CurrentIndex)
            {
                Debug.LogWarning($"{planet.planetName}은(는) 현재 차례가 아닙니다.");
                return;
            }
 
            CurrentIndex++;
            Debug.Log($"{planet.planetName} 정복! 진행도 {CurrentIndex}/{TOTAL_PLANETS}");
 
            if (IsAllCleared)
                Debug.Log("모든 행성 정복 완료 - 엔딩!");
        }
 
        /// <summary>새 게임 시작 시 초기화</summary>
        public static void ResetAll()
        {
            CurrentIndex = 0;
        }
 
        /// <summary>
        /// 진행도를 직접 설정. 디버그/테스트 전용이며 실제 게임 로직에서는 쓰지 말 것.
        /// </summary>
        public static void ForceSetIndex(int index)
        {
            CurrentIndex = Mathf.Clamp(index, 0, TOTAL_PLANETS);
        }
    }
}