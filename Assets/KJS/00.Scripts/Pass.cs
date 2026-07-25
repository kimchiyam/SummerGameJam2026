using UnityEngine;

namespace KJS._00.Scripts
{
    public class Pass : MonoBehaviour
    {
        public void PassButton()
        {
            if (!SystemManager.instance.canMove)
            {
                return;
            }

            if (SystemManager.instance.real)
            {
                CountAlien.TrueAlien();
            }
            else
            {
                CountAlien.FalseAlien(2);
            }

            SystemManager.instance.ExitAilen(true);
        }
    }
}