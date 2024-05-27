using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // PlayerController : 게임오브젝트에 이 스크립트를 넣고 적절한 키설정을 하면 유저가 조종가능한 플레이어가 되도록 하는 클래스 스크립트.
    // wasd와 점프같은 기본적인 이동만 구현할 것.

    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMoveInput;   // 키를 입력할 때마다 여기에 새로운 Vector2값이 들어가게 됨. 방향벡터



    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }





    private void FixedUpdate()
    {
        // OnMove를 통해 얻은 방향을 이용해 계산한 이동메서드가 들어가야함
        Move();
    }

    // InputAction을 통해 얻은 값 curMoveInput을 이용해 벡터방향을 얻고 moveSpeed를 곱해 벡터크기를 얻는다. 
    private void Move()
    {
        // 방향         앞뒤방향 * y값이 -1 ~ +1을 오갈거기 때문.    좌우방향  x값이 -1 ~ +1을 오갈거기 때문
        Vector3 direction = (transform.forward * curMoveInput.y + transform.right * curMoveInput.x).normalized; // 정규화를 더해봄
        direction *= moveSpeed; // 방향에 속도를 곱해서 벡터의 크기를 가진다.
        direction.y = _rb.velocity.y;  // 점프를 했을때만 위아래로 움직여야함.=> 평상시에 움직임에 y(높낮이)가 요동치지 않도록 고정해줌.
        _rb.velocity = direction;
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

}

