using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformLauncher : MonoBehaviour
{
    // 캐릭터가 플랫폼 위에 서 있을 때 특정 방향으로 힘을 가해 발사하는 시스템 구현
    // 특정 키를 누르거나 시간이 경과하면 ForceMode를 사용해 발사
    // 플랫폼발사대를 통해 발사되면 플레이어는 이동입력을 해도 움직이지 못하고 정해진 방향을 향해 날라간다. (시야이동은 가능)


    private float onPlatformTime;   
    public float readyTime;    // 인스펙터에서 설정.
    bool IsShoot = false;

    public Vector3 fixedDirection; //정해진 방향
    public float shootPower;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatformTime = Time.time;
           Debug.Log("On Platform");
        }
    }

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



}
