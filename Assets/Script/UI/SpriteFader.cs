using UnityEngine;
using UnityEngine.UI; // UI.Text 사용을 위한 네임스페이스
using TMPro; // TextMeshProUGUI 사용을 위한 네임스페이스
using System.Collections;

public class SpriteFader : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Fade 인할 SpriteRenderer 참조
    public float fadeDuration = 2.0f; // Fade 인 시간 설정

    // 시작할 때 자동으로 페이드 인을 시작하는 함수 (원하는 경우)

    // Fade 인 시작 함수
    public void StartFadeIn()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FadeIn());
        }
        else
        {
            Debug.LogError("SpriteRenderer not assigned!");
        }
    }

    // 코루틴을 통해 Alpha 값을 서서히 0에서 1로 증가시켜 보이게 만드는 함수
    private IEnumerator FadeIn()
    {
        // 이 오브젝트와 모든 자식 오브젝트의 SpriteRenderer, Image, Text, TextMeshProUGUI 가져오기
        var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var allImages = GetComponentsInChildren<Image>(); // Image 컴포넌트 추가
        var allTextComponents = GetComponentsInChildren<Text>(); // UI.Text
        var allTMPTextComponents = GetComponentsInChildren<TextMeshProUGUI>(); // TextMeshProUGUI

        foreach (var renderer in allSpriteRenderers)
        {
            StartCoroutine(FadeSprite(renderer));
        }

        foreach (var image in allImages)
        {
            StartCoroutine(FadeImage(image)); // Image에 대한 페이드 코루틴 호출
        }

        foreach (var text in allTextComponents)
        {
            StartCoroutine(FadeText(text));
        }

        foreach (var tmpText in allTMPTextComponents)
        {
            StartCoroutine(FadeTMPText(tmpText));
        }

        // 모든 자식이 끝날 때까지 기다림
        yield return null;
    }

    // 개별 SpriteRenderer에 대한 페이드 함수
    private IEnumerator FadeSprite(SpriteRenderer renderer)
    {
        Color startColor = renderer.color;
        startColor.a = 0f; // 초기 알파 값을 0으로 설정
        renderer.color = startColor; // 페이드 시작 전에 알파 값 설정

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            renderer.color = startColor; // 알파 값 조정
            yield return null;
        }

        // 페이드 완료 후 최종적으로 알파 값을 1로 설정
        startColor.a = 1f;
        renderer.color = startColor;
    }

    // Image에 대한 페이드 함수
    private IEnumerator FadeImage(Image image)
    {
        Color startColor = image.color;
        startColor.a = 0f; // 초기 알파 값을 0으로 설정
        image.color = startColor; // 페이드 시작 전에 알파 값 설정

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            image.color = startColor; // 알파 값 조정
            yield return null;
        }

        // 페이드 완료 후 최종적으로 알파 값을 1로 설정
        startColor.a = 1f;
        image.color = startColor;
    }

    // Text에 대한 페이드 함수
    private IEnumerator FadeText(Text text)
    {
        Color startColor = text.color;
        startColor.a = 0f; // 초기 알파 값을 0으로 설정
        text.color = startColor; // 페이드 시작 전에 알파 값 설정

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            text.color = startColor; // 알파 값 조정
            yield return null;
        }

        // 페이드 완료 후 최종적으로 알파 값을 1로 설정
        startColor.a = 1f;
        text.color = startColor;
    }

    // TextMeshProUGUI에 대한 페이드 함수
    private IEnumerator FadeTMPText(TextMeshProUGUI tmpText)
    {
        Color startColor = tmpText.color;
        startColor.a = 0f; // 초기 알파 값을 0으로 설정
        tmpText.color = startColor; // 페이드 시작 전에 알파 값 설정

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            tmpText.color = startColor; // 알파 값 조정
            yield return null;
        }

        // 페이드 완료 후 최종적으로 알파 값을 1로 설정
        startColor.a = 1f;
        tmpText.color = startColor;
    }
}




