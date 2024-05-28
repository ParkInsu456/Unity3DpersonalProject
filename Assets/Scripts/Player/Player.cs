using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Player player { get => CharacterMgr.Instance.Player; }
    [HideInInspector] public PlayerController controller;
    

    private void Awake()
    {
        controller = GetComponent<PlayerController>(); 
    }

}
