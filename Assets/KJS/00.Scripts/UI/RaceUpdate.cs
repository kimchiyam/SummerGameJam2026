using TMPro;
using UnityEngine;

namespace KJS._00.Scripts.UI
{
    public class RaceUpdate : MonoBehaviour
    {
        private TextMeshProUGUI raceText;
        private void Start()
        {
            raceText = GetComponent<TextMeshProUGUI>();
            SystemManager.instance.OnAilenChanged += RaceChange;
        }

        public void RaceChange()
        {
            raceText.text = $"종족 : {SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.race}";
        }
    
    }
}
