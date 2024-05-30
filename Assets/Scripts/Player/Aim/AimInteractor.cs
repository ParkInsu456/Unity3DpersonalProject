using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimInteractor : MonoBehaviour
{
    // hitInfo를 참조해서 hitInfo의 게임오브젝트가 특정인터페이스를 상속하고 있다면 인터페이스의 메서드를 호출하는 클래스

    public AimRaycaster raycaster;
    public IInteractable curInteractable;
    public GameObject prompter; // 인스펙터에서 캐싱

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
