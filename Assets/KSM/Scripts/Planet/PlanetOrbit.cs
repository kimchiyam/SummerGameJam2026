using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class PlanetOrbit : MonoBehaviour
    {
        [Header("공전 설정")]
        public Transform sun;              // 태양 Transform을 드래그해서 연결
        public float orbitSpeed = 20f;     // 공전 속도 (도/초). 행성마다 다르게 주면 실감남
 
        [Header("자전 설정")]
        public float selfRotateSpeed = 0f; // 자전 속도. 스프라이트가 돌면 어색하면 0으로
 
        void Update()
        {
            // 태양 위치를 축으로 Z축(2D 기준) 회전 = 공전
            transform.RotateAround(sun.position, Vector3.forward, orbitSpeed * Time.deltaTime);
 
            // 자전
            if (selfRotateSpeed != 0f)
                transform.Rotate(Vector3.forward, selfRotateSpeed * Time.deltaTime);
        }
    }
}