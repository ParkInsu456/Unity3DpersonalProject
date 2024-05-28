using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    // UI로써의 컨디션바의 출력해야할 현재정보를 받고 출력을 구현할 클래스.
    // Health, Hunger, Stamina 각 바에 캐싱되는 클래스 스크립트.
    // 플레이어에게 Condition을 적용하는건 PlayerCondition클래스에서 함.

    // Hunger는 몬헌처럼 스태미나의 최대치로 묘사하자.

    // 각 컨디션이 가질 속성들. playerCondition이 만들어지면 모두 private하기
    private float curValue;      // 현재 수치
    public float CurValue { get { return curValue; } set { curValue = value; } }
    [SerializeField] private float startValue;    // 초기값
    public float StartValue { get { return startValue; } set { startValue = value; } }
    private float maxValue = 100f;      // 최대 수치
    public float MaxValue { get { return maxValue; } set { maxValue = value; } }
    private float limitHighValue = 250f;
    private float curScale;
    private float limitLowValue = 30f;
    public float passiveValue;  // 시간이 지남에 따라 증감될 수치
    [SerializeField] private Image maxUIBar;      // 최대체력바
    [SerializeField] private Image curUIBar;      // 체력바나 스태미나바

    private void Awake()
    {
        maxUIBar = GetComponent<Image>();
        curUIBar = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        curValue = startValue;
    }

    private void LateUpdate()
    {
        curUIBar.fillAmount = GetPercentage();
    }

    // Image Fill Amount에 적용될 0 ~ 1 값. 매 프레임 확인한다.
    private float GetPercentage()
    {
        return curValue / maxValue;
    }


    // 컨디션 수치 변화에서 호출될 함수
    public void PlusCurrent(float value)
    {
        // curValue + value가 MaxValue를 초과한다면 두 값중 Min값인 MaxValue가 적용될것.
        curValue = Mathf.Min(curValue + value, maxValue); 
    }
    public void MinusCurrent(float value)
    {
        curValue = Mathf.Max(curValue - value, 0);  // 두 값중 큰 값을 반환.
    }

    // 최대수치가 올라갈 경우
    public void PlusMax(float value)
    {
        maxValue = Mathf.Min(maxValue + value, limitHighValue);
        ConditionScale();
    }    
    public void MinusMax(float value)
    {
        maxValue = Mathf.Max(maxValue - value, limitLowValue);  // 두 값중 큰 값을 반환.
        ConditionScale();
    }

    // MaxValue가 변하면 실행될 함수
    private void ConditionScale()
    {
        curScale = maxUIBar.transform.localScale.x;
        curScale *= maxValue * 0.01f;
    }
}
