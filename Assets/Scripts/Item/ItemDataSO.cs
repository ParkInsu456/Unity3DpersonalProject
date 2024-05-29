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
    // �������� �Ӽ��� �����ϴ� ScriptableObject.
    // ����Ƽ ������Ʈ ���¿��� ���� ���� ���� �� �׿� �´� ���̾��Ű�� ���ӿ�����Ʈ�� ĳ���ؾ���.

    [Header("Info")]
    public string itemName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount;  // canStack�� false�� maxStackAmount�� ��ȸ ���ϰ� �������

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;    // ����� �Դ´ٸ� ü�°� ���׹̳� �ΰ����� ȸ���Ѵ�. => �ΰ��� ������ ��ƾ��� => �迭

    [Header("Equip")]
    public GameObject equipPrefab;


}
