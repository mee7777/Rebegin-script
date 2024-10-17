using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalInteraction : MonoBehaviour
{
    public string sceneToLoad = "NextScene";  // �� �̸��� Inspector���� ����
    public Transform spawnPoint;  // �� ������ ĳ���Ͱ� �̵��� ��ġ
    public FadeManager fadeManager;  // FadeManager ����

    public IEnumerator HandlePortalInteraction()
    {
        if (fadeManager != null)
        {
            yield return fadeManager.FadeOut();  // ���̵� �ƿ�
        }

        SceneManager.LoadScene(sceneToLoad);

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (spawnPoint != null)
            {
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                playerTransform.position = spawnPoint.position;  // �� ������ ĳ���� ��ġ ����
            }
        };

        if (fadeManager != null)
        {
            yield return fadeManager.FadeIn();  // ���̵� ��
        }
    }
}
