using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ItemObject : MonoBehaviour, IInteractable
{

    public ItemDataSO data; // 인스펙터에서 넣어줌



    // 여기에 화면에 어떤 모습으로 출력할건지 작성
    public string GetInteractPrompt()
    {
        string str = $"{data.itemName}\n{data.description}";
        return str;
    }

    public void OnInteract()
    {
        // 상호작용 실행
        Player.player.itemData = data;
        Player.player.addItem?.Invoke();
        Destroy(gameObject);    // 눈앞에 있는 아이템을 얻는다면 원래 놓여있던 아이템은 사라지는걸 구현
    }
}
