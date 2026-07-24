using UnityEngine;

namespace KJS._00.Scripts
{
    public class Out : MonoBehaviour
    {
        public void OutButton()
        {
            if (SystemManager.instance.canMove)
            {
                if (SystemManager.instance.real)
                {
                    Debug.Log("진짜 외계인을 아웃했습니다.");
                }
                else
                {
                    Debug.Log("가짜 외계인을 아웃했습니다.");
                }
            }
            else
            {
                Debug.Log("넘어갈수 없습니다.");
            }
        }
    }
}
