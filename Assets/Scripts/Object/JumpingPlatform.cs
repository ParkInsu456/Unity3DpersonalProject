using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    // �����뿡 ����� �� ForceMode.Impulse�� ����� �������� ���� ����
    // �÷����� ���������� �ö󰡼� ���� �����غ���.
    
    Rigidbody rb;

    public float upPower;   // �ν����Ϳ��� ����

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.rigidbody.AddForce(Vector3.up * upPower, ForceMode.Impulse);

            // ������ ��ݷ��� �����ϴ� ���? like �װ������� ĳ����Ʈ
            // ������ �����غ��� ĳ����Ʈ���� �з��� �����ϴ� ������� ĳ����Ʈ�� ���� ����� ���� ������.
            // ĳ����Ʈ�� ���� �����ϸ� ������� ĳ����Ʈ �з����� ���͸� ������ �ϴ÷� ���.
            // �׷��� ĳ����Ʈ�� ���� �з����� ������� �ִϸ��̼��̵� ���簡�� �˾Ƽ� �����̰�
            // ĳ����Ʈ�� �̵��� ������ ������ �����⿡ ĳ����Ʈ��ŭ�� ���͸� �ָ� ���� ������ �ϼ��ȴ�.
            // rb.AddForce(Vector3.up * upPower, ForceMode.Force);
        }
    }
    // 


    //private float onPlatformTime;
    //private float readyTime;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        onPlatformTime = Time.time;
    //        Debug.Log("�����뿡 �ö�");
    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if(Time.time - onPlatformTime > readyTime)
    //    {
    //        Debug.Log("Ready");
    //        //�߻�
    //        other.
    //    }

    //}

}
