using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class OnGameStarted : StateBase<FlowGameManager>
{

    public static Action OnGameStart;
    public OnGameStarted(string stateID, StatesMachine<FlowGameManager> statesMachine) : base(stateID, statesMachine)
    {

    }


    public override void OnEnter(FlowGameManager contex)
    {
        base.OnEnter(contex);
        OnGameStart?.Invoke();
    }

    public override void OnUpdate(FlowGameManager context)
    {
        base.OnUpdate(context);
    }

    public override void OnExit(FlowGameManager contex)
    {
        base.OnExit(contex);


    }

}
