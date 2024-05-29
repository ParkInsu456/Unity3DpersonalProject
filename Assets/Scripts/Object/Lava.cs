using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour, IDealFireElement
{
    public int damage;
    public float damageRate;

    List<IDamageable> things = new List<IDamageable>(); // 1. ���⿡ ����� ������Ʈ�鿡�� ķ�����̾������ �� ��.

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, damageRate);
    }


    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            CalculateFireDamage();  // ��ü���� �Ӽ������� �ٸ��״� �������� ���� ����Ѵ�.
            things[i].TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))   // other�� IDamageable�� �ִٸ� �װ� damageable�� �����´�.
        {
            things.Add(damageable); // 2. ����Ʈ�� ����
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
