using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventMgr : MonoBehaviour
{
    // 이벤트는 여기에 모아서 초기화한다.
    private static EventMgr _instance;
    public static EventMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                // _instance에 ("CharacterMgr")라는 이름의 GameObject를 새로 만든다. 거기에 CharacterMgr컴포넌트를 Add한다.
                _instance = new GameObject("EventMgr").AddComponent<EventMgr>();
            }
            return _instance;
        }
    }

    public AimEvent aimEvent;




    private void Awake()    // 1. Awake()가 실행됐다는건 이 스크립트가 유니티 생명주기에서 들어갔다는것=> GameObject에 붙어서 실행됐다는것.
    {
        if (_instance == null)
        {
            _instance = this;   // 2. 그러니 GameObject를 또 만들 필요는 없음.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        aimEvent = transform.AddComponent<AimEvent>();
    }
}
