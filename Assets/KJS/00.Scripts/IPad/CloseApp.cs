using DG.Tweening;
using UnityEngine;

namespace KJS._00.Scripts.IPad
{
    public class CloseApp : MonoBehaviour
    {
        [SerializeField] private RectTransform app;



        public void CLose_App()
        { 
            app.gameObject.SetActive(true);
        }
    }
}
