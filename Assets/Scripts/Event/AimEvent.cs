using System;
using UnityEngine;

public class AimEvent : MonoBehaviour, IEvent
{
    // AimEvent만을 다루는 클래스
    public static AimEvent myEvent { get => EventMgr.Instance.aimEvent; }   // AimEvent 인스턴스 접근자로 쓰기

    public event Action OnAimEvent;


    public void CallEvent()
    {
        OnAimEvent?.Invoke();

    }

    public void ClearEvent()
    {
        OnAimEvent = null;
    }

    public void AddEvent(Action methodName)
    {
        OnAimEvent += methodName;
    }

    public void RemoveEvent(Action methodName)
    {
        OnAimEvent -= methodName;
    }
}
