using UnityEngine;

namespace KSM.Scripts.Planet
{
    public class SceneBGM : MonoBehaviour
    {
        [Header("이 씬에서 재생할 배경음악")]
        public AudioClip bgmClip;
 
        [Tooltip("반복 재생")]
        public bool loop = true;
 
        [Tooltip("이 씬에서는 음악을 끄고 싶을 때 체크")]
        public bool stopBGM = false;
 
        [Header("전환 방식")]
        [Tooltip("체크하면 이전 곡이 서서히 사라지며 바뀐다")]
        public bool useFade = true;
        public float fadeDuration = 1f;
 
        void Start()
        {
            if (SoundManager.Instance == null)
            {
                Debug.LogWarning("SoundManager가 없습니다. 첫 씬부터 실행했는지 확인하세요.");
                return;
            }
 
            if (stopBGM)
            {
                SoundManager.Instance.StopBGM();
                return;
            }
 
            if (bgmClip == null) return;
 
            if (useFade)
                SoundManager.Instance.PlayBGMFade(bgmClip, fadeDuration, loop);
            else
                SoundManager.Instance.PlayBGM(bgmClip, loop);
        }
    }
}