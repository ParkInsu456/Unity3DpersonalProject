using System;
public interface IEvent
{
    // �̺�Ʈ�� �ʿ��� ��ɵ�
    public void CallEvent();
    public void ClearEvent();
    public void AddEvent(Action methodName);
    public void RemoveEvent(Action methodName);    
}