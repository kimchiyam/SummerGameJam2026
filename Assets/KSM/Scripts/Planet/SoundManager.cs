using UnityEngine;
using UnityEngine.Audio;

namespace KSM.Scripts.Planet
{
    public class SoundManager : MonoBehaviour
    {
         public static SoundManager Instance { get; private set; }
 
        [Header("믹서 연결")]
        [Tooltip("Project에서 만든 AudioMixer를 드래그")]
        public AudioMixer mixer;
 
        [Header("오디오 소스")]
        [Tooltip("배경음악 재생용 (Output을 BGM 그룹으로 설정)")]
        public AudioSource bgmSource;
        [Tooltip("효과음 재생용 (Output을 SFX 그룹으로 설정)")]
        public AudioSource sfxSource;
 
        // PlayerPrefs 저장 키
        const string KEY_MASTER = "vol_master";
        const string KEY_BGM = "vol_bgm";
        const string KEY_SFX = "vol_sfx";
 
        // AudioMixer에 노출시킨 파라미터 이름 (믹서에서 이 이름으로 Expose 해야 함)
        const string PARAM_MASTER = "MasterVolume";
        const string PARAM_BGM = "BGMVolume";
        const string PARAM_SFX = "SFXVolume";
 
        // 0~1 범위의 현재 볼륨값
        public float MasterVolume { get; private set; } = 1f;
        public float BgmVolume { get; private set; } = 1f;
        public float SfxVolume { get; private set; } = 1f;
 
        void Awake()
        {
            // 싱글톤 + 씬 넘어가도 유지
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
 
            LoadSettings();
        }
 
        // ── 설정 불러오기 / 저장 ──────────────────────
 
        /// <summary>저장된 볼륨 설정을 불러와 적용. 처음 실행이면 기본값 사용.</summary>
        void LoadSettings()
        {
            // 저장된 값이 없으면 기본값 (마스터/BGM 0.7, 효과음 0.8)
            MasterVolume = PlayerPrefs.GetFloat(KEY_MASTER, 1f);
            BgmVolume = PlayerPrefs.GetFloat(KEY_BGM, 0.7f);
            SfxVolume = PlayerPrefs.GetFloat(KEY_SFX, 0.8f);
 
            ApplyAll();
        }
 
        void ApplyAll()
        {
            ApplyToMixer(PARAM_MASTER, MasterVolume);
            ApplyToMixer(PARAM_BGM, BgmVolume);
            ApplyToMixer(PARAM_SFX, SfxVolume);
        }
 
        /// <summary>
        /// 0~1 볼륨을 데시벨로 변환해 믹서에 적용.
        /// 소리는 로그 스케일이라 단순 곱셈이 아니라 log10을 써야 자연스럽다.
        /// </summary>
        void ApplyToMixer(string param, float value01)
        {
            if (mixer == null) return;
 
            // 0이면 완전 무음(-80dB), 아니면 로그 변환
            float db = (value01 <= 0.0001f) ? -80f : Mathf.Log10(value01) * 20f;
            mixer.SetFloat(param, db);
        }
 
        // ── 외부에서 호출하는 볼륨 조절 함수 ────────────
 
        public void SetMasterVolume(float value01)
        {
            MasterVolume = Mathf.Clamp01(value01);
            ApplyToMixer(PARAM_MASTER, MasterVolume);
            PlayerPrefs.SetFloat(KEY_MASTER, MasterVolume);
            PlayerPrefs.Save();
        }
 
        public void SetBgmVolume(float value01)
        {
            BgmVolume = Mathf.Clamp01(value01);
            ApplyToMixer(PARAM_BGM, BgmVolume);
            PlayerPrefs.SetFloat(KEY_BGM, BgmVolume);
            PlayerPrefs.Save();
        }
 
        public void SetSfxVolume(float value01)
        {
            SfxVolume = Mathf.Clamp01(value01);
            ApplyToMixer(PARAM_SFX, SfxVolume);
            PlayerPrefs.SetFloat(KEY_SFX, SfxVolume);
            PlayerPrefs.Save();
        }
 
        // ── 재생 함수 ────────────────────────────────
 
        /// <summary>배경음악 재생 (같은 곡이면 다시 시작하지 않음)</summary>
        public void PlayBGM(AudioClip clip, bool loop = true)
        {
            if (bgmSource == null || clip == null) return;
            if (bgmSource.clip == clip && bgmSource.isPlaying) return;
 
            bgmSource.clip = clip;
            bgmSource.loop = loop;
            bgmSource.Play();
        }
 
        /// <summary>
        /// 배경음악을 부드럽게 전환한다. 이전 곡이 서서히 작아진 뒤 새 곡이 커진다.
        /// </summary>
        public void PlayBGMFade(AudioClip clip, float fadeDuration = 1f, bool loop = true)
        {
            if (bgmSource == null || clip == null) return;
            if (bgmSource.clip == clip && bgmSource.isPlaying) return;
 
            StopAllCoroutines();
            StartCoroutine(FadeRoutine(clip, fadeDuration, loop));
        }
 
        System.Collections.IEnumerator FadeRoutine(AudioClip next, float duration, bool loop)
        {
            float half = duration * 0.5f;
            float startVol = bgmSource.volume;
 
            // 1) 페이드 아웃
            if (bgmSource.isPlaying)
            {
                float t = 0f;
                while (t < half)
                {
                    t += Time.unscaledDeltaTime;   // 일시정지 중에도 동작
                    bgmSource.volume = Mathf.Lerp(startVol, 0f, t / half);
                    yield return null;
                }
            }
 
            // 2) 곡 교체
            bgmSource.clip = next;
            bgmSource.loop = loop;
            bgmSource.Play();
 
            // 3) 페이드 인
            float t2 = 0f;
            while (t2 < half)
            {
                t2 += Time.unscaledDeltaTime;
                bgmSource.volume = Mathf.Lerp(0f, startVol, t2 / half);
                yield return null;
            }
            bgmSource.volume = startVol;
        }
 
        public void StopBGM()
        {
            if (bgmSource != null) bgmSource.Stop();
        }
 
        /// <summary>효과음 한 번 재생. 여러 개가 겹쳐도 잘린다거나 하지 않음.</summary>
        public void PlaySFX(AudioClip clip, float volumeScale = 1f)
        {
            if (sfxSource == null || clip == null) return;
            sfxSource.PlayOneShot(clip, volumeScale);
        }
 
        /// <summary>설정 초기화 (기본값으로 되돌리기)</summary>
        public void ResetToDefault()
        {
            SetMasterVolume(1f);
            SetBgmVolume(0.7f);
            SetSfxVolume(0.8f);
        }
    }
}