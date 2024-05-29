public interface IInteractable
{
    // 상호작용 가능한 객체에게 붙는 인터페이스

    // 상호작용에 필요한 기능
    public string GetInteractPrompt();
    public void OnInteract();
    
}