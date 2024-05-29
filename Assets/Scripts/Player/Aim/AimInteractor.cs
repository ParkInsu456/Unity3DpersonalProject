using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimInteractor : MonoBehaviour
{
    // hitInfo�� �����ؼ� hitInfo�� ���ӿ�����Ʈ�� Ư���������̽��� ����ϰ� �ִٸ� �������̽��� �޼��带 ȣ���ϴ� Ŭ����

    AimRaycaster raycaster;

    public TextMeshProUGUI promptText;


    private void Awake()
    {
        raycaster = GetComponent<AimRaycaster>();
    }

    private void Update()
    {
        if (raycaster.IsHit && raycaster.hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable curInteractable))
        {
            promptText.gameObject.SetActive(true);
            promptText.text = curInteractable.GetInteractPrompt();
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }
}
