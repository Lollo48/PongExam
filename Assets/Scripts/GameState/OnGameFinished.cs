using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Mirror;

public class OnGameFinished : StateBase<FlowGameManager>
{
    public static Action OnGameFinish;


    public OnGameFinished(string stateID, StatesMachine<FlowGameManager> statesMachine) : base(stateID, statesMachine)
    {

    }


    public override void OnEnter(FlowGameManager context)
    {
        base.OnEnter(context);
        
    }

    public override void OnUpdate(FlowGameManager context)
    {
        base.OnUpdate(context);

    }


    public override void OnExit(FlowGameManager context)
    {
        base.OnExit(context);

    }


}
