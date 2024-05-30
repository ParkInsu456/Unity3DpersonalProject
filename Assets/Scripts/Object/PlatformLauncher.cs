using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformLauncher : MonoBehaviour
{
    // ĳ���Ͱ� �÷��� ���� �� ���� �� Ư�� �������� ���� ���� �߻��ϴ� �ý��� ����
    // Ư�� Ű�� �����ų� �ð��� ����ϸ� ForceMode�� ����� �߻�
    // �÷����߻�븦 ���� �߻�Ǹ� �÷��̾�� �̵��Է��� �ص� �������� ���ϰ� ������ ������ ���� ���󰣴�. (�þ��̵��� ����)




    private float onPlatformTime;   
    public float readyTime;    // �ν����Ϳ��� ����.

    public Vector3 fixedDirection; //������ ����
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
        if (Time.time - onPlatformTime > readyTime)
        {
            Debug.Log("Ready");

            // �߻�� ���� ���� �������� ������ �÷��̾��� ���͸� �߻纤�ͷ� �ٲ�־����
            
            // Ű��ǲ ����
            //if (other.gameObject.TryGetComponent<PlayerInput>(out PlayerInput playerinput))
            //{
            //    playerinput.enabled = false;
            //}
           
            // ���� �ٲٱ�
            if (other.gameObject.TryGetComponent<PlayerController>(out PlayerController playercontroller))
            {
                playercontroller.ThrowPlayer();                
            }

            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.AddForce(fixedDirection * shootPower, ForceMode.Impulse);
        }

    }





}
