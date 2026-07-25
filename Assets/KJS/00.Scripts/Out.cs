using UnityEngine;

namespace KJS._00.Scripts
{
    public class Out : MonoBehaviour
    {
        public void OutButton()
        {
            if (!SystemManager.instance.canMove)
            {
                return;
            }

            if (SystemManager.instance.real)
            {
            }
            else
            {
            }

            SystemManager.instance.ExitAilen(false);
        }
    }
}