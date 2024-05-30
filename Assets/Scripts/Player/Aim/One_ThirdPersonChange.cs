using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class One_ThirdPersonChange : MonoBehaviour
{
    // cameraContainer에 캐싱한다.
    // 지정된 키를 누르면 플레이어 시점을 1인칭-3인칭으로 바꾼다.

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
            if (cameras[0].gameObject.activeSelf)   // 메인카메라가 켜있으면
            {
                curMaxDistance = aimRaycaster.maxRayDistance;
                
                // 3인칭시점 카메라 켜기
                cameras[0].gameObject.SetActive(false);
                cameras[1].gameObject.SetActive(true);

                // 거리조정, 레이어마스크조정, 
                aimRaycaster.cam = cameras[1];
                aimRaycaster.maxRayDistance = thirdmaxRayDistance;
                //aimRaycaster.layerMask -= LayerMask.NameToLayer("Player");
                //aimRaycaster.layerMask += LayerMask.NameToLayer("Interactable");
            }
            else if(cameras[1].gameObject.activeSelf)
            {
                // 3인칭시점 카메라 켜기
                cameras[0].gameObject.SetActive(true);
                cameras[1].gameObject.SetActive(false);

                // 거리조정, 레이어마스크조정, 
                aimRaycaster.cam = cameras[0];
                aimRaycaster.maxRayDistance = curMaxDistance;
                //aimRaycaster.layerMask += LayerMask.NameToLayer("Player");
            }
        }
    }
}
