using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public enum LayerNum
{
    IgnoreRaycast = 2,
    Ground = 3,
    Interactable = 6,
    Player = 7
}

public class AimRaycaster : MonoBehaviour
{
    // 1��Ī �þ߿��� Ray�� ��� Ŭ����

    private float rayInterval = 0.05f;
    private float lastShootTime;
    public float maxRayDistance = 3f;
    public LayerMask layerMask;    // �ν����ͳ� �ٸ������� ����ũ�� �������ش�.
    public RaycastHit hitInfo;
    public bool IsHit;
    public GameObject curInteractGameObject;

    public Camera cam;

    private bool IsThird = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastShootTime > rayInterval)
        {
            lastShootTime = Time.time;
            FirstPersonRay(cam, maxRayDistance);
        }
    }

    private void FirstPersonRay(Camera _cam,float _maxRayDistance)
    {
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if (Physics.Raycast(ray, out hitInfo, _maxRayDistance, layerMask))
        {
            if (hitInfo.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hitInfo.collider.gameObject;
                Debug.Log("Did hit");
                IsHit = true;
            }
        }
        else
        {
            IsHit = false;
            curInteractGameObject = null;
        }
    }
}
