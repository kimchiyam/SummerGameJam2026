using DG.Tweening;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class HomeUIManager : MonoBehaviour
{
    [Header("SceneChange")]
    [SerializeField] private Image SceneChangeImg;
    [SerializeField] private float moveDistance1 = 1920f; 
    [SerializeField] private float moveDuration1 = 1f;
    
    [Header("Setting")]
    [SerializeField]private Image SettingImg;
    [SerializeField] private float moveDistance2 = 850f;
    [SerializeField] private float moveDuration2 = 1f;
    private bool canClick = true;

    [Header("Skin")]
    [SerializeField] private GameObject AlionSkin;
    [SerializeField] private Image SkinSettingImg;
    [SerializeField] private float moveDistance3 = 443f;
    [SerializeField] private float moveDuration3 = 1f;
    [SerializeField] private Sprite[] skinSprites;
    private bool canClick1 = true;

    private SpriteRenderer targetRenderer;
    //[SerializeField] private  ParticleSystem myParticle;

    private void Awake()
    {
        if (AlionSkin != null)
        {
            targetRenderer = AlionSkin.GetComponent<SpriteRenderer>();
        }
    }
    public void OnStart()
    {
        if (SceneChangeImg == null) return;

        SceneChangeImg.rectTransform.DOAnchorPosX(moveDistance1, moveDuration1)
             .SetRelative()
             .SetEase(Ease.OutQuad)
             .OnComplete(() =>
             {
                
                 SceneManager.LoadScene("CutScene");
             });
    }


    public void SettingOn()
    {
        if (SettingImg == null) return;
        if (canClick == false) return;

        SettingImg.rectTransform.DOAnchorPosX(-moveDistance2, moveDuration2)
            .SetRelative()
            .SetEase(Ease.OutQuad);
        canClick = false;
        
    }

    public void SettingOff()
    {
        if (SettingImg == null) return;
        if (canClick) return;

        SettingImg.rectTransform.DOAnchorPosX(moveDistance2, moveDuration2)
           .SetRelative()
           .SetEase(Ease.OutQuad);
        canClick = true;
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void OnSkinSetting()
    {
        if (SkinSettingImg == null) return;
        if (!canClick1) return;


        SkinSettingImg.rectTransform.DOAnchorPosY(moveDistance3, moveDuration2)
           .SetRelative()
           .SetEase(Ease.OutQuad);
        canClick1 = false;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void OffSkinSetting()
    {
        if (SkinSettingImg == null) return;
        if (canClick1) return;

        SkinSettingImg.rectTransform.DOAnchorPosY(-moveDistance3, moveDuration2)
           .SetRelative()
           .SetEase(Ease.OutQuad);
        canClick1 = true;
    }

    private void ChangeSkin(int index)
    {
        if (skinSprites == null || index < 0 || index >= skinSprites.Length) return;

        if (targetRenderer != null)
        {
            targetRenderer.sprite = skinSprites[index];     
        }
    }

    public void Skin1()
    {
        ChangeSkin(0);
       
    }

    public void Skin2()
    {
        ChangeSkin(1);
        
    }

    public void Skin3()
    {
        ChangeSkin(2);
        
    }

    public void Skin4()
    {
        ChangeSkin(3);
        
    }

    //public void PlayEffect()
    //{
    //    myParticle.Play();
    //}
}
