using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace KSM.Scripts.Planet
{
    public class VolumeSettingsUI : MonoBehaviour
    {
       [Header("슬라이더 (Min 0, Max 1로 설정할 것)")]
        public Slider masterSlider;
        public Slider bgmSlider;
        public Slider sfxSlider;
 
        [Header("퍼센트 표시 (선택)")]
        public TMP_Text masterText;
        public TMP_Text bgmText;
        public TMP_Text sfxText;
 
        [Header("효과음 미리듣기 (선택)")]
        [Tooltip("SFX 슬라이더를 놓았을 때 재생할 샘플 소리")]
        public AudioClip previewClip;
 
        void OnEnable()
        {
            // 패널이 열릴 때마다 현재 저장된 값으로 슬라이더 위치 동기화
            SyncSliders();
        }
 
        void Start()
        {
            // 슬라이더 이벤트 연결 (인스펙터에서 연결해도 되지만 코드로 하면 실수가 없다)
            if (masterSlider != null)
                masterSlider.onValueChanged.AddListener(OnMasterChanged);
 
            if (bgmSlider != null)
                bgmSlider.onValueChanged.AddListener(OnBgmChanged);
 
            if (sfxSlider != null)
            {
                sfxSlider.onValueChanged.AddListener(OnSfxChanged);
                // 슬라이더에서 손을 뗐을 때 미리듣기
                // (EventTrigger 없이 간단히 하려면 아래 PlayPreview를 버튼에 연결해도 됨)
            }
 
            // 모든 Awake가 끝난 뒤이므로 여기서 확실하게 동기화
            SyncSliders();
        }
 
        /// <summary>저장된 값을 슬라이더에 반영</summary>
        public void SyncSliders()
        {
            if (SoundManager.Instance == null)
            {
                // OnEnable이 SoundManager.Awake보다 먼저 돌 수 있다.
                // 이 경우 Start에서 다시 호출되므로 조용히 넘어간다.
                return;
            }
 
            var sm = SoundManager.Instance;
 
            // 이벤트가 다시 발동하지 않도록 SetValueWithoutNotify 사용
            if (masterSlider != null) masterSlider.SetValueWithoutNotify(sm.MasterVolume);
            if (bgmSlider != null) bgmSlider.SetValueWithoutNotify(sm.BgmVolume);
            if (sfxSlider != null) sfxSlider.SetValueWithoutNotify(sm.SfxVolume);
 
            UpdateTexts();
        }
 
        void OnMasterChanged(float v)
        {
            SoundManager.Instance?.SetMasterVolume(v);
            UpdateTexts();
        }
 
        void OnBgmChanged(float v)
        {
            SoundManager.Instance?.SetBgmVolume(v);
            UpdateTexts();
        }
 
        void OnSfxChanged(float v)
        {
            SoundManager.Instance?.SetSfxVolume(v);
            UpdateTexts();
        }
 
        void UpdateTexts()
        {
            if (SoundManager.Instance == null) return;
            var sm = SoundManager.Instance;
 
            if (masterText != null) masterText.text = Mathf.RoundToInt(sm.MasterVolume * 100f) + "%";
            if (bgmText != null) bgmText.text = Mathf.RoundToInt(sm.BgmVolume * 100f) + "%";
            if (sfxText != null) sfxText.text = Mathf.RoundToInt(sm.SfxVolume * 100f) + "%";
        }
 
        /// <summary>효과음 미리듣기. 버튼이나 슬라이더 이벤트에 연결.</summary>
        public void PlayPreview()
        {
            if (previewClip != null)
                SoundManager.Instance?.PlaySFX(previewClip);
        }
 
        /// <summary>기본값 복원 버튼에 연결</summary>
        public void OnResetButton()
        {
            SoundManager.Instance?.ResetToDefault();
            SyncSliders();
        }
    }
}