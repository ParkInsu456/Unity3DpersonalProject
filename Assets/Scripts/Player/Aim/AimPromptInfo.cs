using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimPromptInfo : MonoBehaviour
{
    // Player���ӿ�����Ʈ�� ĳ��
    // Aim���� ������Ʈ�� �ܳ��ϰ� ������ ������Ʈ�� ������ UI�� �ǳ��ִ� Ŭ����
    // ������Ʈ�� ������ FPR���Լ� �޴´�. 
    // ������ Interactable�� ��ü�� ����Ű�� ui�� ���� ������ ����

    // observerŬ����
    [Header("Aim")]
    private FirstPersonRaycaster FPR;
    
    [SerializeField] private LayerMask layerMask;   // �ν����Ϳ��� �����ؾ���.
    

    [Header("UI")]
    public TextMeshProUGUI promptText; // �ν����Ϳ��� ĳ��

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
        promptText.gameObject.SetActive(true);  // ui Ȱ��ȭ
        promptText.text = FPR.curInteractable.GetInteractPrompt();        
    }

    private void DisablePromptText()
    {
        promptText.gameObject.SetActive(false);
    }

    // ��ȣ�ۿ��� �����ϸ� �������� ��°�� ��������� ���������Ѵ�.
    public void OnInteractInput(InputAction.CallbackContext context) // Ű�Էµ����̴ϱ� PlayerController�� �ű�� �͵� �������̴�.
    {
        if (context.phase == InputActionPhase.Started && FPR.curInteractable != null)
        {
            FPR.curInteractable.OnInteract();   // ��ȣ�ۿ��� ����Ǹ� Ray�� ����Ű�°� null�� ����ش�.
            FPR.curAimingObject = null;
            FPR.curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
