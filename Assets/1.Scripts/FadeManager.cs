using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public Image fadeImage;  // ȭ�� ���̵� �ƿ�/�� �̹���
    public float fadeDuration = 1.0f;  // ���̵� �ִϸ��̼� ���� �ð�

    private void Start()
    {
        if (fadeImage != null)
        {
            fadeImage.color = new Color(0, 0, 0, 0);  // �ʱ⿡�� ������ ����
        }
    }

    public IEnumerator FadeOut()
    {
        yield return Fade(1);  // ������ ������
    }

    public IEnumerator FadeIn()
    {
        yield return Fade(0);  // ������ ����
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
