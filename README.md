# Unity3DpersonalProject

 트러블슈팅
 // 이슈 점프구현 다 했는데 점프가 안됨

원인 : 바닥에 닿아있는지 Ray를 발사하는 IsGrounded() 메서드가 있다.

// rays를 순회해 하나라도 땅을 검출해낸다면 true를 반환한다.
```
for(int i = 0; i < rays.Length; ++i)
{
    if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
    {
        return true; 
    }
}
```
// 위에서 true를 반환하지 못한다면 false가 반환된다.
return false;
Physics.Raycast를 통해 Raycast 할 ray[i]와 유효발사거리 0.1f, 검출대상을 지정하고 있다.

Ray를 0.1f 길이만큼 쏘는데, 플레이어로 만든 실린더의 키가 높아 transform의 원점 높이가 0.1f보다 한참 높은 것이었다. 그래서 0.1f길이의 Ray는 땅에 닿지도 못하고 false를 반환하고 있던거였다.

해결 : 
0.1f 길이를 1.1f로 바꿔서 Ray가 충분히 바닥에 닿게했다.


더 좋은 방법? :

Ray의 최대길이를 변수화하고, 이 스크립트에 달린 게임오브젝트의 transform 높이를 얻기(모델링? 메쉬?를 자동으로 감지해서) 원점과 Down방향으로의 길이를 얻어내고 변수에 저장하는 법을 생각했다.


==========

트러블슈팅

1번 : 플랫폼발사기에서 방향대로 조금 가다가 위로만감
1번 원인 : y축vector는 direction.y = _rb.velocity.y; 이 코드를 통해 물리의 이동을 받는데 x,z축은 안받아서 그럼.
해결방법: 날아가는 Addforce가 호출되기 전에 direction.x와 z도 일시적으로 입력의 계산을 제외하고 direction.x =  _rb.velocity.x,
direction.z =  _rb.velocity.z 를 하게했다.


2번 : 플랫폼런처의 앞에 있으면 조금 날아가고 뒤에 있으면 많이 날아간다.
```
private void OnTriggerStay(Collider other)
{
    if (Time.time - onPlatformTime > readyTime)
    {            
        Debug.Log("Ready");

        // 벡터 바꾸기
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playercontroller))
        {
            playercontroller.ThrowPlayer();                
        }

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(fixedDirection * shootPower, ForceMode.Impulse);
    }
}
```
원인: 위 코드대로면 날라가는 도중에 겹쳐있는 시간사이에 코드가 몇번 더 실행된 거였다. 뒤에 있을수록 겹쳐진 시간이 길어서 힘이 여러번 가해지니 날아가는 힘도 세진거다.

해결방법 생각 : 날라가는 판정이 참이 되면 if문이 안돌게 한다.
 
 
```
private void OnTriggerStay(Collider other)
{
    if (!IsShoot && Time.time - onPlatformTime > readyTime)
    {            
        Debug.Log("Ready");
        IsShoot = true;
        StartCoroutine(FalseIsShoot());

        // 벡터 바꾸기
        if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playercontroller))
        {
            playercontroller.ThrowPlayer();                
        }

        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.AddForce(fixedDirection * shootPower, ForceMode.Impulse);
        Debug.Log("Shoot~~");
    }
}

private IEnumerator FalseIsShoot()
{
    yield return new WaitForSeconds(2f);
    IsShoot = false;
    yield return null;
}
```
2번해결: bool값도 같이 체크해서 중복호출되지 않게 했다. bool값은 Invoke로 2초 뒤에 다시 false로 돌아오게 했다. 충분히 발사되고 남을 시간이다. 다시 코루틴으로 바꿨다.

 

이어서 발생한 문제

발사된 후 IsGrounded()가 참이면 벡터를 원래대로 되돌리게 했는데 발사가 된 다음 프레임에서 IsGrounded의 Ray가 땅에 닿아 참이 되어버려 오작동을 일으킨다.
생각 : IsGrounded가 참이 되지 않게 한다. => 발사한 잠시 시간에 IsGrounded의 Ray길이를 짧게 한다.
 

원인찾음 : IsGrounded의 layermask가 Default로 되어있었다. 그리고 발사대 원통도 Default여서 IsGrounded가 참이 나온것이었다......

오히려 잘 날아가던 상황이 있던게 제일 신기하다;;;;;

 

해결 : IsGrounded의 레이어를 변경, 지형의 레이어도 맞춤.
