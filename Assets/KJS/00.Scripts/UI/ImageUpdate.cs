using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KJS._00.Scripts.UI
{
    public class ImageUpdate : MonoBehaviour
    {
        private Image imageha;
        private void Start()
        {
            imageha = GetComponent<Image>();
            SystemManager.instance.OnAilenChanged += ImageChange;
        }

        public void ImageChange()
        {
            imageha.sprite = SystemManager.instance.currentailen.GetComponent<Ailen>()._ailenSo.icon;
        }
    
    }
}
