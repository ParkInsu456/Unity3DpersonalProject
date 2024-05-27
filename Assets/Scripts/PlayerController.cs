using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // PlayerController : ���ӿ�����Ʈ�� �� ��ũ��Ʈ�� �ְ� ������ Ű������ �ϸ� ������ ���������� �÷��̾ �ǵ��� �ϴ� Ŭ���� ��ũ��Ʈ.
    // �⺻���� �̵��� ������ ��. wasd�̵�, ����, ���콺�����̵�

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMoveInput;   // Ű�� �Է��� ������ ���⿡ ���ο� Vector2���� ���� ��. ���⺤��

    [Header("Look")]
    [HideInInspector] public Transform cameraContainer;
    public float minXLook;  // ī�޶� x���� �ִ밢���̴�
    public float maxXLook;
    private float camCurXRot;  // ī�޶��� x���� ����degree
    public float lookSensitivity; // ������ �ΰ���. ���콺 Vector2�� �������
    private Vector2 mouseDelta; // ���콺�� ��Ÿ���� ������ ����. ��Ÿ���� Vector2 �̴�.
    public bool canLook = true; // �κ��丮�� ������ �þ߰� �������� �ʰ�, ���콺Ŀ���� ��������.
    

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        cameraContainer = transform.GetChild(0);    // ���̾��Ű ���� ����
    }





    private void FixedUpdate()
    {
        // OnMove�� ���� ���� ������ �̿��� ����� �̵��޼��尡 ������
        Move();
    }

    private void LateUpdate()
    {
        // OnLook�� ���� ���� Vector2 ���콺��Ÿ���� �̿��� ī�޶� ������ �޼��尡 ������
        CameraLook();
    }

    // InputAction�� ���� ���� �� curMoveInput�� �̿��� ���͹����� ��� moveSpeed�� ���� ����ũ�⸦ ��´�. 
    private void Move()
    {
        // ����         �յڹ��� * y���� -1 ~ +1�� �����ű� ����.    �¿����  x���� -1 ~ +1�� �����ű� ����
        Vector3 direction = (transform.forward * curMoveInput.y + transform.right * curMoveInput.x).normalized; // ����ȭ�� ���غ�
        direction *= moveSpeed; // ���⿡ �ӵ��� ���ؼ� ������ ũ�⸦ ������.
        direction.y = _rb.velocity.y;  // ������ �������� ���Ʒ��� ����������.=> ���ÿ� �����ӿ� y(������)�� �䵿ġ�� �ʵ��� ��������.
        _rb.velocity = direction;
    }

    private void CameraLook()
    {
        // ī�޶� x�� ȸ�� ����
        camCurXRot += mouseDelta.y * lookSensitivity; // x���� �����̴� y���� �ٶ󺻴�. �հ����� ���η� ��� ������ä �ո��� ��������.
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); // Clamp�� ���� ���Ʒ� �ִ밢���� �����Ѵ�.
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0); // ī�޶������̳� ��ü�� ������.  ���콺�� �Ʒ��� ������ -���� ����. �׷��� ��ȣ�� �ٲ���.

        // ī�޶� y�� ȸ�� ����
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    private void RotationYbyCameraLook()
    {

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

}

