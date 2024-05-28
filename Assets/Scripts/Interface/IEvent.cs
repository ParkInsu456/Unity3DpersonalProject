using System;
public interface IEvent
{
    // 이벤트에 필요한 기능들
    public void CallEvent();
    public void ClearEvent();
    public void AddEvent(Action methodName);
    public void RemoveEvent(Action methodName);    
}