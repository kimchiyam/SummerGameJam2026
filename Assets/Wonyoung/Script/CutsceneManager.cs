using DG.Tweening;
using System.Collections;
using KSM.Scripts.Planet;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class CutsceneScene
{
    public Sprite sceneImage;
    [TextArea]
    public string sceneText;
}

public class CutsceneManager : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private Image displayImage;
    [SerializeField] private TextMeshProUGUI displayText;
   
    [Header("컷씬 설정")]
    [SerializeField] private float timePerScene = 5f;
    [SerializeField] private float typingDuration = 2f;
    [SerializeField] private float fadeDuration = 0.4f;

    [Header("컷씬 데이터 (순서대로 추가)")]
    [SerializeField] private CutsceneScene[] cutscenes;

    [SerializeField]private Image image;
    public int sceneNumber = 9;
    void Start()
    {
        PlanetProgress.ResetAll();
        StartCoroutine(PlayCutsceneRoutine());
    }

    private IEnumerator PlayCutsceneRoutine()
    {
        image.rectTransform.DOAnchorPosX(-1920, 1f)
           .SetRelative()
           .SetEase(Ease.OutQuad);

        for (int i = 0; i < cutscenes.Length; i++)
        {
            displayText.DOKill();
            displayText.maxVisibleCharacters = 0;
            displayText.text = "";

            if (i > 0)
            {
                yield return displayImage.DOFade(0f, fadeDuration).WaitForCompletion();
            }
            else
            {
                Color c = displayImage.color;
                c.a = 0f;
                displayImage.color = c;
            }

            if (cutscenes[i].sceneImage != null)
            {
                displayImage.sprite = cutscenes[i].sceneImage;
            }

            yield return displayImage.DOFade(1f, fadeDuration).WaitForCompletion();

            displayText.text = cutscenes[i].sceneText;
            DOTween.To(() => displayText.maxVisibleCharacters,
                       x => displayText.maxVisibleCharacters = x,
                       cutscenes[i].sceneText.Length,
                       typingDuration).SetEase(Ease.Linear);

            yield return new WaitForSeconds(timePerScene);
        }
        SceneChange();
    }

    private void SceneChange()
    {
        image.rectTransform.DOAnchorPosX(1920, 1f)
          .SetRelative()
          .SetEase(Ease.OutQuad)
        .OnComplete(() =>
        {

            SceneManager.LoadScene(sceneNumber);
        });
    }


    public void OnCklickSkip()
    {
        SceneChange();
    }

}