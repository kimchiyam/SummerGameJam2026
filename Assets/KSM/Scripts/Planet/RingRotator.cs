using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class RingRotator : MonoBehaviour
    {
        [Tooltip("고리가 도는 속도 (도/초). 음수면 반대 방향")]
        public float rotateSpeed = 30f;
        void Update()
        {
            // 고리 자체 평면 기준으로 회전 → 알갱이가 궤도를 따라 흐름
            transform.Rotate(transform.position.x, transform.position.y,rotateSpeed * Time.deltaTime, Space.Self);
        }
    }
}