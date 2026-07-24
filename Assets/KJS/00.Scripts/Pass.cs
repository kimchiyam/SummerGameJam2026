using UnityEngine;

namespace KJS._00.Scripts
{
    public class Pass : MonoBehaviour
    {
        public void PassButton()
        {
            if (!SystemManager.instance.canMove)
            {
                Debug.Log("넘어갈 수 없습니다.");
                return;
            }

            if (SystemManager.instance.real)
            {
                Debug.Log("진짜 외계인을 패스했습니다.");
                CountAlien.TrueAlien();
            }
            else
            {
                Debug.Log("가짜 외계인을 패스했습니다.");
                CountAlien.FalseAlien(2);
            }

            SystemManager.instance.ExitAilen(true);
        }
    }
}