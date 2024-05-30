using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // PlayerController : ���ӿ�����Ʈ�� �� ��ũ��Ʈ�� �ְ� ������ Ű������ �ϸ� ������ ���������� �÷��̾ �ǵ��� �ϴ� Ŭ���� ��ũ��Ʈ.
    // �⺻���� �̵��� ������ ��. wasd�̵�, ����, ���콺�����̵�
    // �� ������ �����ϱ� ���� �͵� ������. ������Ʈ���� �ٴڰ��� �Ÿ�

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    public Vector3 direction;
    private Vector2 curMoveInput;   // Ű�� �Է��� ������ ���⿡ ���ο� Vector2���� ���� ��. ���⺤��
    public LayerMask groundLayerMask;   // �ν����Ϳ��� ������ �ְ� �ڵ忡�� ���� ���� �ְڴ�.

    [Header("Look")]
    [HideInInspector] public Transform cameraContainer;
    public float minXLook;  // ī�޶� x���� �ִ밢���̴�
    public float maxXLook;
    private float camCurXRot;  // ī�޶��� x���� ����degree
    public float lookSensitivity; // ������ �ΰ���. ���콺 Vector2�� �������
    private Vector2 mouseDelta; // ���콺�� ��Ÿ���� ������ ����. ��Ÿ���� Vector2 �̴�.
    public bool canLook = true; // �κ��丮�� ������ �þ߰� �������� �ʰ�, ���콺Ŀ���� ��������.

    [Header("Object")]
    // Mesh Renderer�� ���� �޽��� y�� ���̸� ��´�. �� ������ ������ ������Ʈ�� zero������ �ٴڰ��� �Ÿ��� �����Ѵ�.
    private float meshHalfLength;   // �޽��� y�� ����

    // Throwed
    public bool isGrounded { get => IsGrounded(); }
    public bool IsThrowed;

    private PlayerInput playerInput;


    // �����
    public float mouseDeltaY;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        cameraContainer = transform.GetChild(0);    // ���̾��Ű ���� ����
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        GetMeshRendererHalfLengthY();
    }


    private void FixedUpdate()
    {
        // OnMove�� ���� ���� ������ �̿��� ����� �̵��޼��尡 ������
        if (!IsThrowed) 
        { Move(); }
        else 
        { ThrowPlayerEnd(); }
    }

    private void LateUpdate()
    {
        // OnLook�� ���� ���� Vector2 ���콺��Ÿ���� �̿��� ī�޶� ������ �޼��尡 ������
        CameraLook();
        mouseDeltaY = mouseDelta.y;
    }

    // InputAction�� ���� ���� �� curMoveInput�� �̿��� ���͹����� ��� moveSpeed�� ���� ����ũ�⸦ ��´�. 
    private void Move()
    {
        // ����         �յڹ��� * y���� -1 ~ +1�� �����ű� ����.    �¿����  x���� -1 ~ +1�� �����ű� ����
        direction = (transform.forward * curMoveInput.y + transform.right * curMoveInput.x).normalized; // ����ȭ�� ���غ�
        direction *= moveSpeed; // ���⿡ �ӵ��� ���ؼ� ������ ũ�⸦ ������.
        direction.y = _rb.velocity.y; // �̰� ������ ����Ű�� ���� �������� velocity�� �ְ� ���������Ӻ��ʹ� direction.y�� 0�� ��   // ������ �������� ���Ʒ��� ����������.=> ���ÿ� �����ӿ� y(������)�� �䵿ġ�� �ʵ��� ��������.
        _rb.velocity = direction;

    }

    private void CameraLook()
    {
        // ī�޶� x�� ȸ�� ����
        camCurXRot += mouseDelta.y * lookSensitivity; // x���� �����̴� y���� �ٶ󺻴�. �հ����� ���η� ��� ������ä �ո��� ��������.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp�� ���� ���Ʒ� �ִ밢���� �����Ѵ�.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ī�޶������̳� ��ü�� ������.  ���콺�� �Ʒ��� ������ -���� ����. �׷��� ��ȣ�� �ٲ���.

        // ī�޶� y�� ȸ�� ����. �÷��̾���ӿ�����Ʈ�� y���� ������.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    //InvokeUnityEvents�� ���� ȣ��Ǵ� �Լ�
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)    // Ű�� ������ �߿�
        {
            // context���� Vector2���� �����ͼ� �����Ѵ�
            curMoveInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)// Ű�� ���ٸ�
        {
            curMoveInput = Vector2.zero;    // ����Ǵ� �Է°� 0���� �ٲ۴�.
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // ���콺�� ���� �����´�.
        mouseDelta = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
            _rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse); // Vector2�̰� Vector3�̰� y�� ���ϴ°� ����.
    }

    // ���� ����ִٸ� true�� ��ȯ�ϴ� �޼���
    private bool IsGrounded()
    {
        //�÷��̾��� �����¿� 4���� ������ �ٴ����� Ray�� �߻��� Ground���̾ �����س���.
        // �����¿쿡�� �� Ray�� ����� ���ÿ� �ʱ�ȭ
        Ray[] rays = new Ray[4]
        {   
            // transform.up * 0.01f�� Ray�� �������� �ణ ���� �÷��� Raycast�� �������� ����
            new Ray(transform.position + (transform.forward * 0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.forward * -0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.6f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * -0.6f) + (transform.up * 0.01f), Vector3.down),
        };

        // rays�� ��ȸ�� �ϳ��� ���� �����س��ٸ� true�� ��ȯ�Ѵ�.
        for (int i = 0; i < rays.Length; ++i)
        {
            if (Physics.Raycast(rays[i], meshHalfLength + 0.1f, groundLayerMask))
            {
                return true;
            }
        }
        // ������ true�� ��ȯ���� ���Ѵٸ� false�� ��ȯ�ȴ�.
        return false;
    }

    // ����öƩ�ʹ� �ڵ� ����
    private void GetMeshRendererHalfLengthY()
    {
        Renderer mr = GetComponentInChildren<MeshRenderer>();
        if (mr == null) mr = GetComponentInChildren<SkinnedMeshRenderer>();
        meshHalfLength = mr.bounds.size.y / 2;  // �޽��� y���̸� ������ ���� => �߾Ӱ� �ٴڰ��� �Ÿ�
    }


    // direction�� rb�� �°� �ٲ��ִ� �޼���
    public void ThrowPlayer()
    {
        //playerInput.enabled = false;    // ���콺ȸ������ ���� ������ ����. ��ü������ �Ϻθ� ���� ����� ã��.
        playerInput.actions.FindAction("Move").Disable();
        IsThrowed = true;
        direction.x = _rb.velocity.x;
        direction.z = _rb.velocity.z;
    }
    // ���� �� ���� ������ rb�� ������� ������ �޼���
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

