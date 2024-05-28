using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimPromptInfo : MonoBehaviour
{
    // Player���ӿ�����Ʈ�� ĳ��
    // Aim���� ������Ʈ�� �ܳ��ϰ� ������ ������Ʈ�� ������ UI�� ������ִ� Ŭ����
    [Header("Aim")]
    private FirstPersonRaycaster FPR;
    private IInteractable curInteractable;          // FPR���� ������ ���ӿ�����Ʈ�� IInteractable�� ����� �Լ��� �����ϱ� ���� ������.
    [SerializeField] private LayerMask layerMask;   // �ν����Ϳ��� �����ؾ���.

    [Header("UI")]
    public TextMeshProUGUI promptText;

    private void Awake()
    {
        FPR = GetComponent<FirstPersonRaycaster>();
    }

    private void Start()
    {
        AimEvent.myEvent.OnAimEvent += SetPromptText;
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);  // ui Ȱ��ȭ
        promptText.text = curInteractable.GetInteractPrompt();
    }

    // ��ȣ�ۿ��� �����ϸ� �������� ��°�� ��������� ���������Ѵ�.
    public void OnInteractInput(InputAction.CallbackContext context) // Ű�Էµ����̴ϱ� PlayerController�� �ű�� �͵� �������̴�.
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();   // ��ȣ�ۿ��� ����Ǹ� Ray�� ����Ű�°� null�� ����ش�.
            FPR.curAimingObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
