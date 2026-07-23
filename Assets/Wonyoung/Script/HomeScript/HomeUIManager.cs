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

        SettingImg.rectTransform.DOAnchorPosX(moveDistance2, moveDuration2)
           .SetRelative()
           .SetEase(Ease.OutQuad);
        canClick = true;
    }
    public void Exit()
    {
        Application.Quit();
    }


}
