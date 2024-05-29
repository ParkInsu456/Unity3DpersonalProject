using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum ItemType
{
    Equipable,
    Consumable,
    Resource
}

public enum ConsumableType
{
    Health,
    Stamina
}

[Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}


[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/ItemDataSO", order = 1)]
public class ItemDataSO : ScriptableObject
{
    // 아이템의 속성을 정의하는 ScriptableObject.
    // 유니티 프로젝트 에셋에서 만들어서 내용 기입 후 그에 맞는 하이어라키의 게임오브젝트에 캐싱해야함.

    [Header("Info")]
    public string itemName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;  // canStack이 false면 maxStackAmount은 조회 안하게 만들거임

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;    // 당근을 먹는다면 체력과 스테미나 두가지를 회복한다. => 두가지 정보를 담아야함 => 배열

    [Header("Equip")]
    public GameObject equipPrefab;


}
