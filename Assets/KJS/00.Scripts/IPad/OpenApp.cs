using DG.Tweening;
using UnityEngine;

namespace KJS._00.Scripts.IPad
{
    public class OpenApp : MonoBehaviour
    {
        [SerializeField] private RectTransform app;
        [SerializeField] private Vector3 scaleValue;


        public void Open_App()
        { 
            app.gameObject.SetActive(true);
            app.DOScale(scaleValue, 0.1f).OnComplete(() =>
            {
                app.DOScale(new Vector3(1.0f, 1.0f, 1.0f ), 0.1f);
            });
        }
    }
}
