using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompter : MonoBehaviour
{
    // UI오브젝트 Prompter에 캐싱할 클래스

    
    public TextMeshProUGUI promptText;

    public AimInteractor interactor; // 인스펙터에서 캐싱



    private void OnEnable() // 프롬프터 글씨를 보여야 할때.
    {
        AimEvent.myEvent.OnAimEvent += ActivePrompter;
    }

    private void OnDisable()    // 비활성화되면 호출 안해도됨.
    {
        AimEvent.myEvent.OnAimEvent -= ActivePrompter;
    }

    // 프롬프터 출력을 위한 행위
    // 상호작용된 curInteractable에서 GetInteractPrompt를 호출한다.   
    private void ActivePrompter()
    {
        
        promptText.text = interactor.curInteractable.GetInteractPrompt();
    }

}
