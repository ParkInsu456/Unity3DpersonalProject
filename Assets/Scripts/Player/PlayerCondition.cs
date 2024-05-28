using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    // ���ӿ�����Ʈ Player�� ĳ�̵� Ŭ����
    // �÷��̾ ���� ��ġ�� ����. �����
    // ui�� �ٿ� ����Ǿ��ִ� value�� ������ ����.
    

    [SerializeField] private ConditionMgr conditionMgr; // ����Ƽ �ν����Ϳ��� ���� ĳ���ؾ���

    private Condition health 
    {
        get { return conditionMgr.Health; } 
    }
    private Condition stamina
    {
        get { return conditionMgr.Stamina; }
    }







}