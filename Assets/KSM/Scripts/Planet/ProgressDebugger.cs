using UnityEngine;
using UnityEngine.InputSystem;

namespace KSM.Scripts.Planet
{
    public class ProgressDebugger : MonoBehaviour
    {
        [Header("화면에 진행도 표시")]
        public bool showOnScreen = true;
 
        [Header("모든 행성 (화면 표시용, 비워둬도 동작함)")]
        public PlanetSO[] allPlanets;
 
        void Update()
        {
            if (Keyboard.current.yKey.wasPressedThisFrame)
            {
                EndingSequence.Instance.PlayEnding();
            }
            // 다음 행성 해금
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.N))
            {
                AdvanceOne();
            }
 
            // 되돌리기
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.B))
            {
                GoBackOne();
            }
 
            // 초기화
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlanetProgress.ResetAll();
                RefreshAllVisuals();
                Debug.Log("진행도 초기화");
            }
        }
 
        void AdvanceOne()
        {
            if (PlanetProgress.IsAllCleared)
            {
                Debug.Log("이미 모두 정복했습니다.");
                return;
            }
 
            // 현재 차례인 행성을 찾아서 클리어 처리
            PlanetSO target = FindPlanetByIndex(PlanetProgress.CurrentIndex);
 
            if (target != null)
            {
                PlanetProgress.ClearPlanet(target);
            }
            else
            {
                // allPlanets를 안 채웠어도 동작하도록 강제 진행
                PlanetProgress.ForceSetIndex(PlanetProgress.CurrentIndex + 1);
                Debug.Log($"진행도 → {PlanetProgress.CurrentIndex}");
            }
 
            RefreshAllVisuals();
        }
 
        void GoBackOne()
        {
            int newIndex = Mathf.Max(0, PlanetProgress.CurrentIndex - 1);
            PlanetProgress.ForceSetIndex(newIndex);
            Debug.Log($"진행도 → {newIndex}");
            RefreshAllVisuals();
        }
 
        PlanetSO FindPlanetByIndex(int index)
        {
            if (allPlanets == null) return null;
 
            foreach (var p in allPlanets)
            {
                if (p != null && p.planetIndex == index)
                    return p;
            }
            return null;
        }
 
        /// <summary>씬의 모든 행성 겉모습을 다시 그린다</summary>
        void RefreshAllVisuals()
        {
            PlanetVisualState[] visuals = FindObjectsByType<PlanetVisualState>(FindObjectsSortMode.None);
            foreach (var v in visuals)
                v.Refresh();
        }
 
        // 화면 좌상단에 현재 상태 표시
        void OnGUI()
        {
            if (!showOnScreen) return;
 
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 18;
            style.normal.textColor = Color.yellow;
 
            string info = $"진행도: {PlanetProgress.CurrentIndex} / {PlanetProgress.TOTAL_PLANETS}\n";
            info += PlanetProgress.IsAllCleared ? "상태: 전부 정복 (엔딩)\n" : $"현재 열린 행성: index {PlanetProgress.CurrentIndex}\n";
            info += "[→/N] 다음 해금  [←/B] 되돌리기  [R] 초기화";
 
            GUI.Label(new Rect(15, 15, 600, 100), info, style);
        }
    }
    }
