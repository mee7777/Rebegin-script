using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject attackButton;  // 공격 버튼
    public GameObject interactButton;  // 상호작용 버튼
    public GameObject dropButton;
    public Button interactUIButton;  // 상호작용 UI 버튼
    public float interactionDistance = 2.0f;  // 상호작용 거리
    public float requiredHoldTime = 5.0f;  // 버튼을 눌러야 하는 시간 (초)
    public Image progressBar;  // 상호작용 진행 상황을 표시할 이미지

    public bool isInteracting = false;  // 상호작용 중인지 여부
    private float holdTime = 0f;  // 버튼을 누른 시간
    private GameObject closestInteractable = null;
    private PortalInteraction portalInteraction;

    void Start()
    {
        interactUIButton.onClick.AddListener(StartInteraction);  // 상호작용 버튼 클릭 시 상호작용 시작
        UpdateButtons();
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;  // 진행 상태 초기화
        }
    }

    void Update()
    {
        if (isInteracting)
        {
            holdTime += Time.deltaTime;  // 버튼을 누른 시간 측정

            if (progressBar != null)
            {
                progressBar.fillAmount = holdTime / requiredHoldTime;  // 진행 상태 표시
            }

            if (holdTime >= requiredHoldTime)
            {
                if (portalInteraction != null)
                {
                    StartCoroutine(portalInteraction.HandlePortalInteraction());  // 포탈 상호작용
                }
                StopInteraction();  // 상호작용 완료 후 종료
            }
        }
        else
        {
            holdTime = 0f;  // 상호작용이 아닌 경우 시간 초기화
            if (progressBar != null)
            {
                progressBar.fillAmount = 0f;  // 진행 상태 초기화
            }
        }

        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        float closestDistance = interactionDistance;
        closestInteractable = null;
        portalInteraction = null;

        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Weapon");
        GameObject[] portals = GameObject.FindGameObjectsWithTag("Portal");

        foreach (GameObject interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        foreach (GameObject portal in portals)
        {
            float distance = Vector3.Distance(transform.position, portal.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = portal;
                portalInteraction = portal.GetComponent<PortalInteraction>();
            }
        }

        if (closestInteractable != null)
        {
            interactButton.SetActive(true);
            attackButton.SetActive(false);
        }
        else
        {
            interactButton.SetActive(false);
            attackButton.SetActive(true);
        }
    }


    public void StartInteraction()
    {
        if (closestInteractable != null)
        {
            isInteracting = true;  // 상호작용 시작
        }
    }

    public void StopInteraction()
    {
        isInteracting = false;  // 상호작용 중지
        holdTime = 0f;  // 시간 초기화
    }


    void UpdateButtons()
    {
        // 필요한 버튼 업데이트 로직
    }
}
