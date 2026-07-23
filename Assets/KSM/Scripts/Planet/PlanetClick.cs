using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class PlanetClick : MonoBehaviour
    {
        public string planetName = "수성"; // 인스펙터에서 행성 이름 입력
 
        void OnMouseDown()
        {
            // UI 위를 클릭한 경우는 무시 (패널 버튼 누를 때 행성이 같이 클릭되는 것 방지)
            if (UnityEngine.EventSystems.EventSystem.current != null &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
 
            // 1) 오른쪽 침략 패널 띄우기
            InvadePanelUI.Instance.Show(planetName);
 
            // 2) 카메라가 이 행성을 따라가게
            PlanetCameraController.Instance.FocusOn(transform);
        }
    }
    
}