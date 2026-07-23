using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class PlanetClick : MonoBehaviour
    {
        public string planetName = "수성"; // 인스펙터에서 행성 이름 입력
 
        void OnMouseDown()
        {
            // 클릭하면 오른쪽 침략 패널을 띄운다
            InvadePanelUI.Instance.Show(planetName);
        }
    }
    
}