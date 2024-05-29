using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public enum LayerNum
{
    Default = 0,
    Interactable =6
}

public class FirstPersonRaycaster : MonoBehaviour
{
    // Player게임오브젝트에 캐싱되는 클래스.
    // ㅇㄹ


    [Header("Aim Ray")]
    private float checkInterval = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask layerMask;   // 인스펙터에서 설정해야함. 여기에 지형을 뺀 나머지를 모두 넣자?

    public GameObject curAimingObject;
    public IInteractable curInteractable;          
    public RaycastHit curHit;

    private Camera mainCamera;

    public bool IsThird = false;
    [Header("UI")]
    public TextMeshProUGUI promptText;

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        // 여기서 레이캐스팅 함수를 호출한다.
        if (!IsThird)
        Raycast();
    }


    private void Raycast()
    {
        if(Time.time - lastCheckTime > checkInterval)
        {
            lastCheckTime = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit; //Ray를 맞은 대상을 저장할 변수
            
            if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))   // layerMask에 해당하는 오브젝트를 ray 맞추면 true
            {
                if (hit.collider.gameObject != curAimingObject)  // 계속 같은 오브젝트를 가리키면 달라질게 없기때문에 아래코드를 실행하지 않아도됨. 처음엔 비어있을것
                {
                    curAimingObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                }
            }
            // else if() 다른 레이어
            
            else // layerMask가 아니다.
            {
                curAimingObject = null;
                curInteractable = null;
            }

        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context) // 키입력동작이니까 PlayerController로 옮기는 것도 좋을것이다.
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();   // 상호작용이 실행되면 Ray가 가리키는걸 null로 비워준다.
            curAimingObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
