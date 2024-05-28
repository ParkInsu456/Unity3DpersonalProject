using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMgr : MonoBehaviour
{
    private static CharacterMgr _instance;
    public static CharacterMgr Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new GameObject("CharacterMgr").AddComponent<CharacterMgr>();
            }
            return _instance;
        }
    }

    public Player _player;
    public Player Player
    {
        get { return _player; }
        set { _player = value; }
    }

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
    }
}
