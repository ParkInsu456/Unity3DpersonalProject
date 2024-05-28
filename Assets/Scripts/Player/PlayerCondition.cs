using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    // 게임오브젝트 Player에 캐싱될 클래스
    // 플레이어가 가질 수치적 정보. 컨디션
    // ui의 바에 저장되어있는 value를 가져다 쓴다.
    

    [SerializeField] private ConditionMgr conditionMgr; // 유니티 인스펙터에서 직접 캐싱해야함

    private Condition health 
    {
        get { return conditionMgr.Health; } 
    }
    private Condition stamina
    {
        get { return conditionMgr.Stamina; }
    }







}