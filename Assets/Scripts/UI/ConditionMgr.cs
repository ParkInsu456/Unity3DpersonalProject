using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMgr : MonoBehaviour
{
    // ui로써의 하위 게임오브젝트 체력과 스태미너에의 접근 창구를 하는 클래스
    // Condition의 단일정보Value를 health, stamina로 추상적으로 구분해서 가리킬 수 있게 함.

    private Condition _health;
    public Condition Health {  get { return _health; } set { _health = value; } }
    private Condition _stamina;
    public Condition Stamina { get {  return _stamina; } set {  _stamina = value; } }

    private void Awake()
    {
        _health = transform.GetChild(0).GetComponent<Condition>();
        _stamina = transform.GetChild(1).GetComponent<Condition>();
    }

    private void Start()
    {
        
    }

}
