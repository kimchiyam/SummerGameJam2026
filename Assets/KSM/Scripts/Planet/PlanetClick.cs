using System;
using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class PlanetClick : MonoBehaviour
    {
        public PlanetSO planet;
 
        [Tooltip("잠긴 행성도 클릭해서 정보를 볼 수 있게 할지")]
        public bool allowClickOnLocked = true;
 
        void OnMouseDown()
        {
            if (UnityEngine.EventSystems.EventSystem.current != null &&
                UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;
 
            if (planet == null)
            {
                Debug.LogError(gameObject.name + " 의 planet SO가 비어있습니다.");
                return;
            }
 
            // 잠긴 행성 클릭을 막고 싶으면 여기서 차단
            if (!allowClickOnLocked && !PlanetProgress.CanEnter(planet))
                return;
 
            InvadePanelUI.Instance.Show(planet);
            PlanetCameraController.Instance.FocusOn(transform);
        }
    }
    
}