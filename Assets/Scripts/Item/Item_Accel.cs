using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IFieldItem
{
    public IEnumerator ActiveFieldItem();
}

public class Item_Accel : MonoBehaviour , IFieldItem
{
    public float value;     // �ν����Ϳ��� ����
    public float duration;  // �ν����Ϳ��� ����

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Get FieldItem");
            StartCoroutine(ActiveFieldItem());
        }
    }


    public IEnumerator ActiveFieldItem()
    {
        float curMoveSpeed = Player.player.GetmoveSpeed();
        Player.player.SetmoveSpeed(value);
        yield return new WaitForSeconds(duration);
        Player.player.SetmoveSpeed(curMoveSpeed);
        yield return null;
    }

}
