using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PortalInteraction : MonoBehaviour
{
    public string sceneToLoad = "NextScene";  // 씬 이름을 Inspector에서 설정
    public Transform spawnPoint;  // 새 씬에서 캐릭터가 이동할 위치
    public FadeManager fadeManager;  // FadeManager 참조

    public IEnumerator HandlePortalInteraction()
    {
        if (fadeManager != null)
        {
            yield return fadeManager.FadeOut();  // 페이드 아웃
        }

        SceneManager.LoadScene(sceneToLoad);

        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (spawnPoint != null)
            {
                Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                playerTransform.position = spawnPoint.position;  // 새 씬에서 캐릭터 위치 설정
            }
        };

        if (fadeManager != null)
        {
            yield return fadeManager.FadeIn();  // 페이드 인
        }
    }
}
