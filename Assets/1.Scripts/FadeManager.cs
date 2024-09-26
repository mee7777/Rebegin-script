using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;  // 화면 페이드 아웃/인 이미지
    public float fadeDuration = 1.0f;  // 페이드 애니메이션 지속 시간

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);  // 초기에는 완전히 투명
        }
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(1);  // 완전히 불투명
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(0);  // 완전히 투명
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, targetAlpha);
    }
}
