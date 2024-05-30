using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    // 점프대에 닿았을 때 ForceMode.Impulse를 사용해 순간적인 힘을 가함
    // 플랫폼이 순간적으로 올라가서 힘을 전달해보자.
    
    Rigidbody rb;

    public float upPower;   // 인스펙터에서 설정

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(Vector3.up * upPower, ForceMode.Impulse);
            
            // 발판의 충격력을 전달하는 방법? like 항공모함의 캐터펄트
            // 실제를 생각해보면 캐터펄트에게 밀려져 가속하는 전투기는 캐터펄트와 같은 방향과 힘을 가진다.
            // 캐터펄트가 끝에 도달하면 전투기는 캐터펄트 밀려지던 벡터를 가지고 하늘로 뜬다.
            // 그러면 캐터펄트에 의해 밀려지는 전투기는 애니메이션이든 뭐든가에 알아서 움직이고
            // 캐터펄트의 이동이 끝나는 순간에 전투기에 캐터펄트만큼의 벡터를 주면 같은 원리가 완성된다.
            // rb.AddForce(Vector3.up * upPower, ForceMode.Force);
        }
    }
   

}
