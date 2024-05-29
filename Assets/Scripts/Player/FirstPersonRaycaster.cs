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
    // 플레이어 게임오브젝트에 캐싱되는 클래스.

    // 크로스헤어를 경로로 Ray를 쏘아 오브젝트와 상호작용의 틀을 만든 클래스 ( 상호작용키(상호작용의 내용은 따로 구현))
    // 상호작용된 게임오브젝트에 접근해 그 오브젝트의 인터페이스 메서드를 호출한다.

    // 원래 클래스에서 화면가운데서 Ray발사와 검출, 검출대상에 대한 표시도 수행하고있었다. 여기서 검출대상에 대한 표시는 xx클래스로 분리한다.
    // 검출대상에 따른 수행은 Action에 담아 이벤트로 호출한다. 여러 클래스에서 여러 상황에 따라 이 이벤트에 체인한다.
    // 이렇게 해서 에임으로 가리켰을때 대상정보표시만이 아닌 바라보는것만으로의 상호작용이나 그외 여러가지 구현을 기대한다.

    // RaycastHit로 얻은 게임오브젝트를 저장

    // 크로스헤어를 경로로 Ray를 쏘는 클래스

    // 옵저버패턴: Subject클래스

    [Header("Aim Ray")]
    private float checkInterval = 0.05f;
    private float lastCheckTime;
    [SerializeField] private float maxRayDistance;
    [SerializeField] private LayerMask layerMask;   // 인스펙터에서 설정해야함. 여기에 지형을 뺀 나머지를 모두 넣자?

    public GameObject curAimingObject;
    public IInteractable curInteractable;          // FPR에서 검출한 게임오브젝트의 IInteractable에 선언된 함수로 접근하기 위한 접근자.
    public RaycastHit curHit;

    private Camera mainCamera;
    

    private void Awake()
    {
        mainCamera = Camera.main;
    }


    private void Update()
    {
        // 여기서 레이캐스팅 함수를 호출한다.
        FirstPersonRaycast();
    }


    private void FirstPersonRaycast()
    {
        if(Time.time - lastCheckTime > checkInterval)
        {
            lastCheckTime = Time.time;

            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit; //Ray를 맞은 대상을 저장할 변수
            
            if (Physics.Raycast(ray, out hit, maxRayDistance))
            {
                if (hit.collider.gameObject != curAimingObject)  // 계속 같은 오브젝트를 가리키면 달라질게 없기때문에 아래코드를 실행하지 않아도됨. 처음엔 비어있을것, 에임대상이 이벤트 대상이 아니면 CallEvent에 뭐가 들어있지도 않을것.
                {
                    curAimingObject = hit.collider.gameObject;
                    layerMask.value = hit.collider.gameObject.layer;
                    switch (layerMask.value)
                    {
                        case (int)LayerNum.Interactable:
                            curInteractable = hit.collider.GetComponent<IInteractable>();

                            AimEvent.myEvent.CallEvent();   // 정보를 출력하는 함수를 넣는다.    이 객체가 검출됐다고 옵저버에 알려줌
                            break;
                        default:
                            layerMask.value = -1;
                            curAimingObject = null;
                            curInteractable = null;

                            break;
                    }
                    //// 여기에 옵저버패턴?

                }
            }
            else //    //Ray에 검출된 대상이 없는 경우는 왠만하면 없다. 땅을 향하면 땅이 검출된다.
            {
                //curAimingObject = null;
                //curInteractable = null;
                // 여기에 옵저버패턴?
                //AimEvent.myEvent.ClearEvent();
            }

        }

    }


}
