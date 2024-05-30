using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class One_ThirdPersonChange : MonoBehaviour
{
    // cameraContainer�� ĳ���Ѵ�.
    // ������ Ű�� ������ �÷��̾� ������ 1��Ī-3��Ī���� �ٲ۴�.

    private Camera[] cameras;

    AimRaycaster aimRaycaster;
   

    public float thirdmaxRayDistance = 10f;
    float curMaxDistance;

    private void Awake()
    {
        cameras = GetComponentsInChildren<Camera>(true);
    }

    private void Start()
    {
        aimRaycaster = Player.player.GetComponent<AimRaycaster>();
        
    }

    public void ChangePersonView(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started)
        {            
            if (cameras[0].gameObject.activeSelf)   // ����ī�޶� ��������
            {
                curMaxDistance = aimRaycaster.maxRayDistance;
                
                // 3��Ī���� ī�޶� �ѱ�
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);

                // �Ÿ�����, ���̾��ũ����, 
                aimRaycaster.cam = cameras[1];
                aimRaycaster.maxRayDistance = thirdmaxRayDistance;
                //aimRaycaster.layerMask -= LayerMask.NameToLayer("Player");
                //aimRaycaster.layerMask += LayerMask.NameToLayer("Interactable");
            }
            else if(cameras[1].gameObject.activeSelf)
            {
                // 3��Ī���� ī�޶� �ѱ�
                cameras[0].gameObject.SetActive(true);
                cameras[1].gameObject.SetActive(false);

                // �Ÿ�����, ���̾��ũ����, 
                aimRaycaster.cam = cameras[0];
                aimRaycaster.maxRayDistance = curMaxDistance;
                //aimRaycaster.layerMask += LayerMask.NameToLayer("Player");
            }
        }
    }
}
