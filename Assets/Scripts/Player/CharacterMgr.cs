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
    }
}
