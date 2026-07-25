using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace KSM.Scripts.Planet
{
    public class SettingCloseAndOpen : MonoBehaviour
    {
        [SerializeField] private GameObject settingView;
        public bool isOpen;
        private void Start()
        {
            settingView.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
            {
                isOpen = !isOpen;              // 상태를 뒤집고
                settingView.SetActive(isOpen); // 그 값에 맞춰 켜고 끔
            }
        }

        public void CloseSettings()
        {
            isOpen = false;
            settingView.SetActive(false);
        }

        public void ExitBtn()
        {
            // 1. 현재 씬의 Timer를 찾아서 텍스트 끄기
            var timer = FindObjectOfType<Timer>();
            if (timer != null) timer.ResetTimer();

            // 2. DontDestroyOnLoad로 살아있는 DayManager 정리
            if (DayManager.Instance != null)
                Destroy(DayManager.Instance.gameObject);

            // 3. 메인 씬으로 이동
            SceneManager.LoadScene(0);
        }
    }
}