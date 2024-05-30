using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 플레이어가 가지고 있어야할 정보들. 입력, 컨디션, 소유아이템
    
    public static Player player { get => CharacterMgr.Instance.Player; }

    [HideInInspector] public PlayerController controller;
    [HideInInspector] public PlayerCondition condition;

    public ItemDataSO itemData;   // itemObject에서 정보를 넣어줌.
    public Action addItem;

    private void Awake()
    {
        CharacterMgr.Instance.Player = this;
        controller = GetComponent<PlayerController>(); 
        condition = GetComponent<PlayerCondition>();
    }


    public float GetmoveSpeed()
    {
        return controller.moveSpeed;
    }
    public void SetmoveSpeed(float value)
    {
        controller.moveSpeed = value;
    }
    

}
