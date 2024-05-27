using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // PlayerController : ���ӿ�����Ʈ�� �� ��ũ��Ʈ�� �ְ� ������ Ű������ �ϸ� ������ ���������� �÷��̾ �ǵ��� �ϴ� Ŭ���� ��ũ��Ʈ.
    // wasd�� �������� �⺻���� �̵��� ������ ��.

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMoveInput;   // Ű�� �Է��� ������ ���⿡ ���ο� Vector2���� ���� ��. ���⺤��



    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }





    private void FixedUpdate()
    {
        // OnMove�� ���� ���� ������ �̿��� ����� �̵��޼��尡 ������
        Move();
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

}

