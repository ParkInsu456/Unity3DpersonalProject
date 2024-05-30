using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // PlayerController : 게임오브젝트에 이 스크립트를 넣고 적절한 키설정을 하면 유저가 조종가능한 플레이어가 되도록 하는 클래스 스크립트.
    // 기본적인 이동만 구현할 것. wasd이동, 점프, 마우스시점이동
    // 위 사항을 구현하기 위한 것도 들어가있음. 오브젝트에서 바닥과의 거리

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public Vector3 direction;
    private Vector2 curMoveInput;   // 키를 입력할 때마다 여기에 새로운 Vector2값이 들어가게 됨. 방향벡터
    public LayerMask groundLayerMask;   // 인스펙터에서 고를수도 있고 코드에서 정할 수도 있겠다.

    [Header("Look")]
    [HideInInspector] public Transform cameraContainer;
    public float minXLook;  // 카메라 x축의 최대각도이다
    public float maxXLook;
    private float camCurXRot;  // 카메라의 x축의 각도degree
    public float lookSensitivity; // 움직임 민감도. 마우스 Vector2의 증폭계수
    private Vector2 mouseDelta; // 마우스의 델타값을 저장할 변수. 델타값은 Vector2 이다.
    public bool canLook = true; // 인벤토리를 켰을땐 시야가 움직이지 않고, 마우스커서가 보여야함.

    [Header("Object")]
    // Mesh Renderer를 통해 메쉬의 y축 길이를 얻는다. 그 길이의 절반을 오브젝트의 zero원점과 바닥과의 거리로 간주한다.
    private float meshHalfLength;   // 메쉬의 y축 길이

    // Throwed
    public bool isGrounded { get => IsGrounded(); }
    public bool IsThrowed;

    private PlayerInput playerInput;


    // 디버그
    public float mouseDeltaY;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        cameraContainer = transform.GetChild(0);    // 하이어라키 순서 주의
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        GetMeshRendererHalfLengthY();
    }


    private void FixedUpdate()
    {
        // OnMove를 통해 얻은 방향을 이용해 계산한 이동메서드가 들어가야함
        if (!IsThrowed) 
        { Move(); }
        else 
        { ThrowPlayerEnd(); }
    }

    private void LateUpdate()
    {
        // OnLook을 통해 얻은 Vector2 마우스델타값을 이용해 카메라를 돌리는 메서드가 들어가야함
        CameraLook();
        mouseDeltaY = mouseDelta.y;
    }

    // InputAction을 통해 얻은 값 curMoveInput을 이용해 벡터방향을 얻고 moveSpeed를 곱해 벡터크기를 얻는다. 
    private void Move()
    {
        // 방향         앞뒤방향 * y값이 -1 ~ +1을 오갈거기 때문.    좌우방향  x값이 -1 ~ +1을 오갈거기 때문
        direction = (transform.forward * curMoveInput.y + transform.right * curMoveInput.x).normalized; // 정규화를 더해봄
        direction *= moveSpeed; // 방향에 속도를 곱해서 벡터의 크기를 가진다.
        direction.y = _rb.velocity.y; // 이거 없으면 점프키를 누른 순간에만 velocity가 있고 다음프레임부터는 direction.y가 0이 됨   // 점프를 했을때만 위아래로 움직여야함.=> 평상시에 움직임에 y(높낮이)가 요동치지 않도록 고정해줌.
        _rb.velocity = direction;

    }

    private void CameraLook()
    {
        // 카메라 x축 회전 구현
        camCurXRot += mouseDelta.y * lookSensitivity; // x축은 움직이는 y점을 바라본다. 손가락을 가로로 펴고 유지한채 손목을 돌려보자.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp를 통해 위아래 최대각도를 구현한다.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // 카메라컨테이너 자체를 돌린다.  마우스를 아래로 내리면 -값이 나옴. 그래서 부호를 바꿔줌.

        // 카메라 y축 회전 구현. 플레이어게임오브젝트의 y축을 돌린다.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    //InvokeUnityEvents를 통해 호출되는 함수
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)    // 키를 누르는 중에
        {
            // context에서 Vector2값을 가져와서 저장한다
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)// 키를 뗀다면
        {
            curMoveInput = Vector2.zero;    // 저장되는 입력값 0으로 바꾼다.
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // 마우스의 값을 가져온다.
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // Vector2이건 Vector3이건 y가 변하는건 같다.
    }

    // 땅에 닿아있다면 true를 반환하는 메서드
    private bool IsGrounded()
    {
        //플레이어의 전후좌우 4개의 점에서 바닥으로 Ray를 발사해 Ground레이어를 검출해낸다.
        // 전후좌우에서 쏠 Ray를 선언과 동시에 초기화
        Ray[] rays = new Ray[4]
        {   
            // transform.up * 0.01f로 Ray의 시작점을 약간 위로 올려서 Raycast의 안정성을 높임
            new Ray(transform.position + (transform.forward * 0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.forward * -0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * -0.6f) + (transform.up * 0.01f), Vector3.down),
        };

        // rays를 순회해 하나라도 땅을 검출해낸다면 true를 반환한다.
        for (int i = 0; i < rays.Length; ++i)
        {
            if (Physics.Raycast(rays[i], meshHalfLength + 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        // 위에서 true를 반환하지 못한다면 false가 반환된다.
        return false;
    }

    // 소재철튜터님 코드 참고
    private void GetMeshRendererHalfLengthY()
    {
        Renderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr == null) mr = GetComponentInChildren<SkinnedMeshRenderer>();
        meshHalfLength = mr.bounds.size.y / 2;  // 메쉬의 y길이를 반으로 나눔 => 중앙과 바닥과의 거리
    }


    // direction을 rb에 맞게 바꿔주는 메서드
    public void ThrowPlayer()
    {
        //playerInput.enabled = false;    // 마우스회전까지 막는 문제가 있음. 구체적으로 일부만 막는 방법을 찾기.
        playerInput.actions.FindAction("Move").Disable();
        IsThrowed = true;
        direction.x = _rb.velocity.x;
        direction.z = _rb.velocity.z;
    }
    // 날라간 후 땅에 닿으면 rb를 원래대로 돌리는 메서드
    public void ThrowPlayerEnd()
    {
        if(isGrounded)
        {
            //playerInput.enabled = true;
            playerInput.actions.FindAction("Move").Enable();
            IsThrowed = false;
        }
    }
}

