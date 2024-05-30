using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompter : MonoBehaviour
{
    // UI������Ʈ Prompter�� ĳ���� Ŭ����

    
    public TextMeshProUGUI promptText;

    public AimInteractor interactor; // �ν����Ϳ��� ĳ��



    private void OnEnable() // �������� �۾��� ������ �Ҷ�.
    {
        AimEvent.myEvent.OnAimEvent += ActivePrompter;
    }

    private void OnDisable()    // ��Ȱ��ȭ�Ǹ� ȣ�� ���ص���.
    {
        AimEvent.myEvent.OnAimEvent -= ActivePrompter;
    }

    // �������� ����� ���� ����
    // ��ȣ�ۿ�� curInteractable���� GetInteractPrompt�� ȣ���Ѵ�.   
    private void ActivePrompter()
    {
        
        promptText.text = interactor.curInteractable.GetInteractPrompt();
    }

}
