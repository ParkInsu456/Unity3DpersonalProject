using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemObject : MonoBehaviour, IInteractable
{

    public ItemDataSO data; // �ν����Ϳ��� �־���



    // ���⿡ ȭ�鿡 � ������� ����Ұ��� �ۼ�
    public string GetInteractPrompt()
    {
        string str = $"{data.itemName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        // ��ȣ�ۿ� ����
        Player.player.itemData = data;
        Player.player.addItem?.Invoke();
        Destroy(gameObject);    // ���տ� �ִ� �������� ��´ٸ� ���� �����ִ� �������� ������°� ����
    }
}
