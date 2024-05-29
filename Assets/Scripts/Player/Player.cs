using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // �÷��̾ ������ �־���� ������. �Է�, �����, ����������
    
    public static Player player { get => CharacterMgr.Instance.Player; }

    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerCondition condition;

    public ItemDataSO itemData;   // itemObject���� ������ �־���.
    public Action addItem;

    private void Awake()
    {
        controller = GetComponent<PlayerController>(); 
        condition = GetComponent<PlayerCondition>();
    }

}
