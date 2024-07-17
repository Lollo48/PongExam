using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class OnGameFinished : StateBase<FlowGameManager>
{
    public static event Action OnGameFinish;


    public OnGameFinished(string stateID, StatesMachine<FlowGameManager> statesMachine) : base(stateID, statesMachine)
    {

    }


    public override void OnEnter(FlowGameManager context)
    {
        base.OnEnter(context);
        OnGameFinish?.Invoke();
        NetworkManager.singleton.StopHost();

    }
}
