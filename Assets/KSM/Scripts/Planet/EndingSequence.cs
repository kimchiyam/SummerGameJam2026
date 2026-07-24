using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KSM.Scripts.Planet
{
    public class EndingSequence : MonoBehaviour
    {
        public static EndingSequence Instance;
        [Header("대상")]
        public Transform sun;
        [Tooltip("빨려 들어갈 행성들 (전부 드래그)")]
        public Transform[] planets;
 
        [Header("1막: 흡수")]
        [Tooltip("행성이 태양까지 빨려 들어가는 시간")]
        public float absorbDuration = 2.5f;
        [Tooltip("행성들이 순서대로 출발하는 간격")]
        public float absorbInterval = 0.35f;
        [Tooltip("나선 강도. 0이면 직선으로 빨려 들어감")]
        public float spiralAmount = 2.5f;
        [Tooltip("행성 하나 먹을 때마다 태양이 커지는 비율")]
        public float growPerPlanet = 0.12f;
 
        [Header("2막: 팽창")]
        public float swellDuration = 2.5f;
        [Tooltip("폭발 직전 태양 크기 배율")]
        public float maxSwellScale = 1.8f;
        [Tooltip("팽창 중 떨림 세기")]
        public float shakeStrength = 0.15f;
        public Color swellColor = Color.white;   // 폭발 직전 색

        [Header("3막: 폭발")]
        [Tooltip("폭발 순간 재생할 파티클 (미리 만들어두고 드래그, 비활성 상태로)")]
        public ParticleSystem explosionParticle;
        [Tooltip("화면 전체를 덮는 흰색 UI Image (Canvas에 만들고 드래그, 비활성 상태로)")]
        public Image flashImage;
        public float flashDuration = 0.3f;
 
        [Header("마무리")]
        [Tooltip("폭발 후 대기 시간")]
        public float afterExplosionWait = 2f;
        [Tooltip("엔딩 씬 빌드 인덱스. -1이면 씬 이동 없음")]
        public int endingSceneIndex = -1;
 
        bool playing;
        public GameObject line;
 
        void Awake() { Instance = this; }

        /// <summary>★ 엔딩 시작. 모든 행성 정복 후 호출 ★</summary>

        private void Start()
        {
            if (PlanetProgress.IsAllCleared)
            {
                PlayEnding();
            }
        }

        public void PlayEnding()
        {
            if (playing) return;
            playing = true;
            StartCoroutine(EndingRoutine());
        }
 
        IEnumerator EndingRoutine()
        {
            // 공전/클릭 등 기존 동작 정지
            StopAllPlanetMotion();
 
            // ── 1막: 행성 흡수 ──────────────────────
            List<Coroutine> absorbs = new List<Coroutine>();
            foreach (Transform p in planets)
            {
                if (p == null) continue;
                absorbs.Add(StartCoroutine(AbsorbPlanet(p)));
                yield return new WaitForSeconds(absorbInterval);
            }
            // 모든 흡수가 끝날 때까지 대기
            yield return new WaitForSeconds(absorbDuration);
 
            // ── 2막: 태양 팽창 + 떨림 ────────────────
            yield return StartCoroutine(SwellSun());
 
            // ── 3막: 폭발 ───────────────────────────
            yield return StartCoroutine(Explode());
 
            // ── 마무리 ─────────────────────────────
            yield return new WaitForSeconds(afterExplosionWait);
 
            if (endingSceneIndex >= 0)
                UnityEngine.SceneManagement.SceneManager.LoadScene(endingSceneIndex);
        }
 
        /// <summary>행성 하나가 나선을 그리며 태양으로 빨려 들어간다</summary>
        IEnumerator AbsorbPlanet(Transform planet)
        {
            Vector3 startPos = planet.position;
            Vector3 startScale = planet.localScale;
 
            // 태양 기준 시작 각도와 거리
            Vector3 offset = startPos - sun.position;
            float startAngle = Mathf.Atan2(offset.y, offset.x);
            float startDist = offset.magnitude;
 
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / absorbDuration;
                float ease = t * t; // 갈수록 빨라짐 (중력 느낌)
 
                // 거리는 줄고, 각도는 돌면서 = 나선
                float dist = Mathf.Lerp(startDist, 0f, ease);
                float angle = startAngle + ease * spiralAmount;
 
                planet.position = sun.position + new Vector3(
                    Mathf.Cos(angle) * dist,
                    Mathf.Sin(angle) * dist,
                    0f);
 
                // 빨려 들어가며 작아짐
                planet.localScale = startScale * (1f - ease * 0.7f);
 
                yield return null;
            }
 
            // 도착: 행성 숨기고 태양 커지기
            planet.gameObject.SetActive(false);
            StartCoroutine(GrowSun(growPerPlanet));
        }
 
        /// <summary>태양이 잠깐 사이에 살짝 커진다 (꿀꺽 느낌)</summary>
        IEnumerator GrowSun(float amount)
        {
            Vector3 from = sun.localScale;
            Vector3 to = from * (1f + amount);
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / 0.3f;
                sun.localScale = Vector3.Lerp(from, to, t);
                yield return null;
            }
        }
 
        /// <summary>태양 팽창 + 떨림 + 색 변화</summary>
        IEnumerator SwellSun()
        {
            Vector3 baseScale = sun.localScale;
            Vector3 basePos = sun.position;
 
            SpriteRenderer sr = sun.GetComponent<SpriteRenderer>();
            Color baseColor = sr != null ? sr.color : Color.white;
 
            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime / swellDuration;
 
                // 크기 팽창
                sun.localScale = baseScale * Mathf.Lerp(1f, maxSwellScale, t);
 
                // 떨림 (갈수록 심해짐)
                float shake = shakeStrength * t;
                sun.position = basePos + (Vector3)(Random.insideUnitCircle * shake);
 
                // 색 변화
                if (sr != null)
                    sr.color = Color.Lerp(baseColor, swellColor, t);
 
                yield return null;
            }
                line.SetActive(false);
            sun.position = basePos;
        }
       
        /// <summary>플래시 + 폭발 파티클 + 태양 제거</summary>
        IEnumerator Explode()
        {
            // 폭발 파티클 재생
            if (explosionParticle != null)
            {
                explosionParticle.transform.position = sun.position;
                explosionParticle.gameObject.SetActive(true);
                explosionParticle.Play();
            }
 
            // 태양 숨기기
            sun.gameObject.SetActive(false);
 
            // 흰색 플래시: 확 밝아졌다가 서서히 사라짐
            if (flashImage != null)
            {
                flashImage.gameObject.SetActive(true);
 
                // 즉시 흰색으로
                flashImage.color = Color.white;
                yield return new WaitForSeconds(flashDuration);
 
                // 서서히 투명하게
                float t = 0f;
                while (t < 1f)
                {
                    t += Time.deltaTime / 1.2f;
                    flashImage.color = new Color(1f, 1f, 1f, 1f - t);
                    yield return null;
                }
                flashImage.gameObject.SetActive(false);
            }
        }
 
        /// <summary>공전, 클릭, 맥박 등 기존 스크립트 정지</summary>
        void StopAllPlanetMotion()
        {
            foreach (var orbit in FindObjectsByType<PlanetOrbit>(FindObjectsSortMode.None))
                orbit.enabled = false;
 
            foreach (var click in FindObjectsByType<PlanetClick>(FindObjectsSortMode.None))
                click.enabled = false;
 
            foreach (var visual in FindObjectsByType<PlanetVisualState>(FindObjectsSortMode.None))
                visual.enabled = false;
        }
    }
}