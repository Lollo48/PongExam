using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    
    public FlowGameManager flowGame { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        flowGame = FindObjectOfType<FlowGameManager>();
    }

}
