using System.Collections;
using UnityEngine;

public class FadeAnimation : MonoBehaviour {
    public float fadeInDuration = 0.2f;
    public float fadeOutDuration = 0.2f;

    private CanvasGroup canvasGroup;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null) {
            Debug.LogError("ScoreAnimation script requires a CanvasGroup component.");
            return;
        }

        canvasGroup.alpha = 0;
    }

    public void PlayFullAnimation() {
        StartCoroutine(FadeInAndOut());
    }

    public void PlayFadeIn() {
        StartCoroutine(FadeIn());
    }

    public void PlayFadeOut() {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn() {
        float elapsedTime = 0;

        while (elapsedTime < fadeInDuration) {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeInDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1;
    }

    private IEnumerator FadeOut() {
        float elapsedTime = 0;

        while (elapsedTime < fadeOutDuration) {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0;
    }

    private IEnumerator FadeInAndOut() {
        yield return StartCoroutine(FadeIn());
        yield return new WaitForSeconds(0.5f);
        yield return StartCoroutine(FadeOut());
    }
}