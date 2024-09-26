using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject attackButton;  // ���� ��ư
    public GameObject interactButton;  // ��ȣ�ۿ� ��ư
    public GameObject dropButton;
    public Button interactUIButton;  // ��ȣ�ۿ� UI ��ư
    public float interactionDistance = 2.0f;  // ��ȣ�ۿ� �Ÿ�
    public float requiredHoldTime = 5.0f;  // ��ư�� ������ �ϴ� �ð� (��)
    public Image progressBar;  // ��ȣ�ۿ� ���� ��Ȳ�� ǥ���� �̹���

    public bool isInteracting = false;  // ��ȣ�ۿ� ������ ����
    private float holdTime = 0f;  // ��ư�� ���� �ð�
    private GameObject closestInteractable = null;
    private PortalInteraction portalInteraction;

    void Start()
    {
        interactUIButton.onClick.AddListener(StartInteraction);  // ��ȣ�ۿ� ��ư Ŭ�� �� ��ȣ�ۿ� ����
        UpdateButtons();
        if (progressBar != null)
        {
            progressBar.fillAmount = 0f;  // ���� ���� �ʱ�ȭ
        }
    }

    void Update()
    {
        if (isInteracting)
        {
            holdTime += Time.deltaTime;  // ��ư�� ���� �ð� ����

            if (progressBar != null)
            {
                progressBar.fillAmount = holdTime / requiredHoldTime;  // ���� ���� ǥ��
            }

            if (holdTime >= requiredHoldTime)
            {
                if (portalInteraction != null)
                {
                    StartCoroutine(portalInteraction.HandlePortalInteraction());  // ��Ż ��ȣ�ۿ�
                }
                StopInteraction();  // ��ȣ�ۿ� �Ϸ� �� ����
            }
        }
        else
        {
            holdTime = 0f;  // ��ȣ�ۿ��� �ƴ� ��� �ð� �ʱ�ȭ
            if (progressBar != null)
            {
                progressBar.fillAmount = 0f;  // ���� ���� �ʱ�ȭ
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
            isInteracting = true;  // ��ȣ�ۿ� ����
        }
    }

    public void StopInteraction()
    {
        isInteracting = false;  // ��ȣ�ۿ� ����
        holdTime = 0f;  // �ð� �ʱ�ȭ
    }


    void UpdateButtons()
    {
        // �ʿ��� ��ư ������Ʈ ����
    }
}
