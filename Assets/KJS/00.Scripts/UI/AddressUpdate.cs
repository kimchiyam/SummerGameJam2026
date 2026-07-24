using TMPro;
using UnityEngine;

namespace KJS._00.Scripts.UI
{
    public class AddressUpdate : MonoBehaviour
    {
        private TextMeshProUGUI addressText;
        private void Start()
        {
            addressText = GetComponent<TextMeshProUGUI>();
            SystemManager.instance.OnAilenChanged += AddressChange;
        }

        public void AddressChange()
        {
            addressText.text = $"주소 : {SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.address}";
        }
    
    }
}
