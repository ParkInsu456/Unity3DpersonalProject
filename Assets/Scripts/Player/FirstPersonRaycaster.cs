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
    // Player���ӿ�����Ʈ�� ĳ�̵Ǵ� Ŭ����.
    // ����


    [Header("Aim Ray")]
    private float checkInterval = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask layerMask;   // �ν����Ϳ��� �����ؾ���. ���⿡ ������ �� �������� ��� ����?

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
        // ���⼭ ����ĳ���� �Լ��� ȣ���Ѵ�.
        if (!IsThird)
        Raycast();
    }


    private void Raycast()
    {
        if(Time.time - lastCheckTime > checkInterval)
        {
            lastCheckTime = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit; //Ray�� ���� ����� ������ ����
            
            if (Physics.Raycast(ray, out hit, maxRayDistance, layerMask))   // layerMask�� �ش��ϴ� ������Ʈ�� ray ���߸� true
            {
                if (hit.collider.gameObject != curAimingObject)  // ��� ���� ������Ʈ�� ����Ű�� �޶����� ���⶧���� �Ʒ��ڵ带 �������� �ʾƵ���. ó���� ���������
                {
                    curAimingObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                }
            }
            // else if() �ٸ� ���̾�
            
            else // layerMask�� �ƴϴ�.
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

    public void OnInteractInput(InputAction.CallbackContext context) // Ű�Էµ����̴ϱ� PlayerController�� �ű�� �͵� �������̴�.
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();   // ��ȣ�ۿ��� ����Ǹ� Ray�� ����Ű�°� null�� ����ش�.
            curAimingObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
