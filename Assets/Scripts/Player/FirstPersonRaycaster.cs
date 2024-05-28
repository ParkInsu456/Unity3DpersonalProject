using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum LayerNum
{
    Default = 0,
    Interactable =6
}

public class FirstPersonRaycaster : MonoBehaviour
{
    // �÷��̾� ���ӿ�����Ʈ�� ĳ�̵Ǵ� Ŭ����.

    // ũ�ν��� ��η� Ray�� ��� ������Ʈ�� ��ȣ�ۿ��� Ʋ�� ���� Ŭ���� ( ��ȣ�ۿ�Ű(��ȣ�ۿ��� ������ ���� ����))
    // ��ȣ�ۿ�� ���ӿ�����Ʈ�� ������ �� ������Ʈ�� �������̽� �޼��带 ȣ���Ѵ�.

    // ���� Ŭ�������� ȭ�鰡��� Ray�߻�� ����, ������ ���� ǥ�õ� �����ϰ��־���. ���⼭ ������ ���� ǥ�ô� xxŬ������ �и��Ѵ�.
    // ������ ���� ������ Action�� ��� �̺�Ʈ�� ȣ���Ѵ�. ���� Ŭ�������� ���� ��Ȳ�� ���� �� �̺�Ʈ�� ü���Ѵ�.
    // �̷��� �ؼ� �������� ���������� �������ǥ�ø��� �ƴ� �ٶ󺸴°͸������� ��ȣ�ۿ��̳� �׿� �������� ������ ����Ѵ�.

    // RaycastHit�� ���� ���ӿ�����Ʈ�� ����

    // ũ�ν��� ��η� Ray�� ��� Ŭ����

    [Header("Aim Ray")]
    private float checkInterval = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask layerMask;   // �ν����Ϳ��� �����ؾ���.

    public GameObject curAimingObject;
    public RaycastHit curHit;

    private Camera mainCamera;

   

    

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        // ���⼭ ����ĳ���� �Լ��� ȣ���Ѵ�.
        FirstPersonRaycast();
    }


    private void FirstPersonRaycast()
    {
        if(Time.time - lastCheckTime > checkInterval)
        {
            lastCheckTime = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit; //Ray�� ���� ����� ������ ����

            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                if(hit.collider.gameObject != curAimingObject)  // ó���� ���������, ���Ӵ���� �̺�Ʈ ����� �ƴϸ� CallEvent�� ���� ��������� ������.
                {
                    curAimingObject = hit.collider.gameObject;
                    switch (layerMask.value)
                    {
                        case (int)LayerNum.Interactable :
                            AimEvent.myEvent.CallEvent();   // ������ ����ϴ� �Լ��� �ִ´�.
                            break;
                    }
                    // ���⿡ ����������?
                    
                }
            }
            else // ũ�ν��� �� ������ �����Ѽ� Ray�� ����� ����� ���� ���
            {
                curAimingObject = null;
                // ���⿡ ����������?
                AimEvent.myEvent.ClearEvent();
            }

        }

    }


}
