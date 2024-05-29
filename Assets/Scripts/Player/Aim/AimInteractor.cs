using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AimInteractor : MonoBehaviour
{
    // hitInfo를 참조해서 hitInfo의 게임오브젝트가 특정인터페이스를 상속하고 있다면 인터페이스의 메서드를 호출하는 클래스

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
