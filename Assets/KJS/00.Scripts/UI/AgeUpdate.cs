using TMPro;
using UnityEngine;

namespace KJS._00.Scripts.UI
{
    public class AgeUpdate : MonoBehaviour
    {
        private TextMeshProUGUI ageText;
        private void Start()
        {
            ageText = GetComponent<TextMeshProUGUI>();
            SystemManager.instance.OnAilenChanged += AgeChange;
        }

        public void AgeChange()
        {
            ageText.text = $"나이 : {SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.age}";
        }
    
    }
}
