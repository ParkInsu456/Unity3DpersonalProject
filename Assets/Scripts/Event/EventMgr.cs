using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventMgr : MonoBehaviour
{
    // �̺�Ʈ�� ���⿡ ��Ƽ� �ʱ�ȭ�Ѵ�.
    private static EventMgr _instance;
    public static EventMgr Instance
    {
        get
        {
            if (_instance == null)
            {
                // _instance�� ("CharacterMgr")��� �̸��� GameObject�� ���� �����. �ű⿡ CharacterMgr������Ʈ�� Add�Ѵ�.
                _instance = new GameObject("EventMgr").AddComponent<EventMgr>();
            }
            return _instance;
        }
    }

    public AimEvent aimEvent;




    private void Awake()    // 1. Awake()�� ����ƴٴ°� �� ��ũ��Ʈ�� ����Ƽ �����ֱ⿡�� ���ٴ°�=> GameObject�� �پ ����ƴٴ°�.
    {
        if (_instance == null)
        {
            _instance = this;   // 2. �׷��� GameObject�� �� ���� �ʿ�� ����.
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
