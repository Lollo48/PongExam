using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OnPreGameStarted : StateBase<FlowGameManager>
{

    public static event Action<float> OnPreGameUpdate;
    public static event Action OnPreGameEnd;
    public static event Action OnPreGameStart;

    private float _timer;

    public OnPreGameStarted(string stateID, StatesMachine<FlowGameManager> statesMachine) : base(stateID, statesMachine)
    {

    }

    public override void OnEnter(FlowGameManager context)
    {
        base.OnEnter(context);
        _timer = 5;
        OnPreGameStart?.Invoke();
    }



    public override void OnUpdate(FlowGameManager context)
    {
        base.OnUpdate(context);

        
        _timer -= Time.deltaTime;
        OnPreGameUpdate?.Invoke(_timer);

        if (_timer <= 0)
        {
            FlowGameManager.FlowGame.ChangeState(context.OnGameStarted);
        }
    }



    public override void OnExit(FlowGameManager context)
    {
        base.OnExit(context);
        OnPreGameEnd?.Invoke();
    }
}
