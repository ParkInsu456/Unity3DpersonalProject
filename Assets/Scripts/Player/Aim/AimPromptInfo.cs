using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimPromptInfo : MonoBehaviour
{
    // Player게임오브젝트에 캐싱
    // Aim으로 오브젝트에 겨냥하고 있을때 오브젝트의 정보를 UI로 건네주는 클래스
    // 오브젝트의 정보는 FPR에게서 받는다. 
    // 에임이 Interactable한 객체를 가리키면 ui를 통해 정보를 띄운다

    // observer클래스
    [Header("Aim")]
    private FirstPersonRaycaster FPR;
    
    [SerializeField] private LayerMask layerMask;   // 인스펙터에서 설정해야함.
    

    [Header("UI")]
    public TextMeshProUGUI promptText; // 인스펙터에서 캐싱

    private void Awake()
    {
        FPR = GetComponent<FirstPersonRaycaster>();
    }

    private void Start()
    {
        AimEvent.myEvent.OnAimEvent += SetPromptText;
    }

    private void Update()
    {
        if (FPR.curInteractable == null)
        {
            DisablePromptText();
        }
        



    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);  // ui 활성화
        promptText.text = FPR.curInteractable.GetInteractPrompt();        
    }

    private void DisablePromptText()
    {
        promptText.gameObject.SetActive(false);
    }

    // 상호작용을 실행하면 아이템을 얻는경우 정보출력이 없어져야한다.
    public void OnInteractInput(InputAction.CallbackContext context) // 키입력동작이니까 PlayerController로 옮기는 것도 좋을것이다.
    {
        if (context.phase == InputActionPhase.Started && FPR.curInteractable != null)
        {
            FPR.curInteractable.OnInteract();   // 상호작용이 실행되면 Ray가 가리키는걸 null로 비워준다.
            FPR.curAimingObject = null;
            FPR.curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
