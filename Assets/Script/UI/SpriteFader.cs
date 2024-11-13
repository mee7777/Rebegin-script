using UnityEngine;
using UnityEngine.UI; // UI.Text ����� ���� ���ӽ����̽�
using TMPro; // TextMeshProUGUI ����� ���� ���ӽ����̽�
using System.Collections;

public class SpriteFader : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Fade ���� SpriteRenderer ����
    public float fadeDuration = 2.0f; // Fade �� �ð� ����

    // ������ �� �ڵ����� ���̵� ���� �����ϴ� �Լ� (���ϴ� ���)

    // Fade �� ���� �Լ�
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

    // �ڷ�ƾ�� ���� Alpha ���� ������ 0���� 1�� �������� ���̰� ����� �Լ�
    private IEnumerator FadeIn()
    {
        // �� ������Ʈ�� ��� �ڽ� ������Ʈ�� SpriteRenderer, Image, Text, TextMeshProUGUI ��������
        var allSpriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        var allImages = GetComponentsInChildren<Image>(); // Image ������Ʈ �߰�
        var allTextComponents = GetComponentsInChildren<Text>(); // UI.Text
        var allTMPTextComponents = GetComponentsInChildren<TextMeshProUGUI>(); // TextMeshProUGUI

        foreach (var renderer in allSpriteRenderers)
        {
            StartCoroutine(FadeSprite(renderer));
        }

        foreach (var image in allImages)
        {
            StartCoroutine(FadeImage(image)); // Image�� ���� ���̵� �ڷ�ƾ ȣ��
        }

        foreach (var text in allTextComponents)
        {
            StartCoroutine(FadeText(text));
        }

        foreach (var tmpText in allTMPTextComponents)
        {
            StartCoroutine(FadeTMPText(tmpText));
        }

        // ��� �ڽ��� ���� ������ ��ٸ�
        yield return null;
    }

    // ���� SpriteRenderer�� ���� ���̵� �Լ�
    private IEnumerator FadeSprite(SpriteRenderer renderer)
    {
        Color startColor = renderer.color;
        startColor.a = 0f; // �ʱ� ���� ���� 0���� ����
        renderer.color = startColor; // ���̵� ���� ���� ���� �� ����

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            renderer.color = startColor; // ���� �� ����
            yield return null;
        }

        // ���̵� �Ϸ� �� ���������� ���� ���� 1�� ����
        startColor.a = 1f;
        renderer.color = startColor;
    }

    // Image�� ���� ���̵� �Լ�
    private IEnumerator FadeImage(Image image)
    {
        Color startColor = image.color;
        startColor.a = 0f; // �ʱ� ���� ���� 0���� ����
        image.color = startColor; // ���̵� ���� ���� ���� �� ����

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            image.color = startColor; // ���� �� ����
            yield return null;
        }

        // ���̵� �Ϸ� �� ���������� ���� ���� 1�� ����
        startColor.a = 1f;
        image.color = startColor;
    }

    // Text�� ���� ���̵� �Լ�
    private IEnumerator FadeText(Text text)
    {
        Color startColor = text.color;
        startColor.a = 0f; // �ʱ� ���� ���� 0���� ����
        text.color = startColor; // ���̵� ���� ���� ���� �� ����

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            text.color = startColor; // ���� �� ����
            yield return null;
        }

        // ���̵� �Ϸ� �� ���������� ���� ���� 1�� ����
        startColor.a = 1f;
        text.color = startColor;
    }

    // TextMeshProUGUI�� ���� ���̵� �Լ�
    private IEnumerator FadeTMPText(TextMeshProUGUI tmpText)
    {
        Color startColor = tmpText.color;
        startColor.a = 0f; // �ʱ� ���� ���� 0���� ����
        tmpText.color = startColor; // ���̵� ���� ���� ���� �� ����

        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration)
        {
            timeElapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(timeElapsed / fadeDuration);
            startColor.a = alpha;
            tmpText.color = startColor; // ���� �� ����
            yield return null;
        }

        // ���̵� �Ϸ� �� ���������� ���� ���� 1�� ����
        startColor.a = 1f;
        tmpText.color = startColor;
    }
}




