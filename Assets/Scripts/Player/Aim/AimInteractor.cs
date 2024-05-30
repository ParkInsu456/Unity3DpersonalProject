using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimInteractor : MonoBehaviour
{
    // hitInfo�� �����ؼ� hitInfo�� ���ӿ�����Ʈ�� Ư���������̽��� ����ϰ� �ִٸ� �������̽��� �޼��带 ȣ���ϴ� Ŭ����

    public AimRaycaster raycaster;
    public IInteractable curInteractable;
    public GameObject prompter; // �ν����Ϳ��� ĳ��

    private void Awake()
    {
        raycaster = GetComponent<AimRaycaster>();
    }

    private void Update()
    {
        if (raycaster.IsHit && raycaster.hitInfo.collider.gameObject.TryGetComponent<IInteractable>(out curInteractable))
        {
            prompter.gameObject.SetActive(true);
            AimEvent.myEvent.CallEvent(); 
        }
        else
        {
            prompter.gameObject.SetActive(false);
        }
    }
}
