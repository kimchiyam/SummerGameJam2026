using DG.Tweening;
using UnityEngine;

namespace KJS._00.Scripts.IPad
{
    public class MoveButton : MonoBehaviour
    {
        [SerializeField] private GameObject[] pages;
        private int index = 0;
        
        public void Move_NextPage()
        {
            if (index >= pages.Length - 1)
            {
                Debug.Log("넘어갈수없습니다.");
            }
            else
            {
                index++;
                pages[index].SetActive(true);
                pages[index - 1].SetActive(false);
            }
        }
        
        public void Move_PreviousPage()
        { 
            if (index <= 0)
            {
                Debug.Log("넘어갈수없습니다.");
            }
            else
            {
                index--;
                pages[index].SetActive(true);
                pages[index + 1].SetActive(false);
            }
        }
    }
}
