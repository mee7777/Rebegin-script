using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public void OpenDoor()
    {
        // 문이 열리는 동작 (애니메이션 또는 이동)
        Debug.Log("문이 열립니다!");
        // 여기에 문을 열리는 코드 추가 (예: Transform 이동 또는 애니메이션 실행)
        gameObject.SetActive(false);  // 예시로 문을 비활성화하여 열리는 효과
    }
}
