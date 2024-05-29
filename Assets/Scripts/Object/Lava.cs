using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, IDealFireElement
{
    public int damage;
    public float damageRate;

    List<IDamageable> things = new List<IDamageable>(); // 1. 여기에 저장된 오브젝트들에게 캠프파이어데미지를 줄 것.

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }


    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            CalculateFireDamage();  // 객체마다 속성방어력이 다를테니 데미지를 따로 계산한다.
            things[i].TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))   // other가 IDamageable이 있다면 그걸 damageable로 가져온다.
        {
            things.Add(damageable); // 2. 리스트에 저장
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            things.Remove(damageable);
        }
    }

    
    public void CalculateFireDamage()
    {
        damage += 1;
    }
}
