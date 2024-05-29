using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class AimRaycaster : MonoBehaviour
{
    // 1��Ī �þ߿��� Ray�� ��� Ŭ����

    private float rayInterval = 0.05f;
    private float lastShootTime;
    private float maxRayDistance = 3f;
    public LayerMask layerMask;    // �ν����ͳ� �ٸ������� ����ũ�� �������ش�.
    public RaycastHit hitInfo;
    public bool IsHit;
    public GameObject curInteractGameObject;

    Camera cam;

    
    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastShootTime > rayInterval)
        {
            lastShootTime = Time.time;
            FirstPersonRay();
        }
    }

    private void FirstPersonRay()
    {
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        
        if (Physics.Raycast(ray, out hitInfo, maxRayDistance, layerMask))
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
