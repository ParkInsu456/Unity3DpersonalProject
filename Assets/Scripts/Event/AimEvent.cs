using System;
using UnityEngine;

public class AimEvent : MonoBehaviour, IEvent
{
    // AimEvent���� �ٷ�� Ŭ����
    public static AimEvent myEvent { get => EventMgr.Instance.aimEvent; }

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
