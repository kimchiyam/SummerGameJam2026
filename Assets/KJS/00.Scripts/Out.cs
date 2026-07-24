using UnityEngine;

namespace KJS._00.Scripts
{
    public class Out : MonoBehaviour
    {
        public void OutButton()
        {
            if (!SystemManager.instance.canMove)
            {
                Debug.Log("넘어갈 수 없습니다.");
                return;
            }

            if (SystemManager.instance.real)
            {
                Debug.Log("진짜 외계인을 아웃했습니다.");
            }
            else
            {
                Debug.Log("가짜 외계인을 아웃했습니다.");
            }

            SystemManager.instance.ExitAilen(false);
        }
    }
}