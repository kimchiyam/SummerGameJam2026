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
            SceneManager.LoadScene(0);
            
        }
    }
}