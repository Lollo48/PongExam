using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowGameManager : MonoBehaviour
{
    public StatesMachine<FlowGameManager> FlowGame;


    #region States
    public OnPreGameStarted OnPreGameStarted;
    public OnGameStarted OnGameStarted;
    public OnGameFinished OnGameFinished;
    #endregion



    private void Awake()
    {
        InitStateMachine();
    }

    private void Update()
    {
        FlowGame.CurrentState.OnUpdate(this);
    }

    private void InitStateMachine()
    {
        FlowGame = new StatesMachine<FlowGameManager>(this);
        OnPreGameStarted = new OnPreGameStarted("OnPreGameStarted", FlowGame);
        OnGameStarted = new OnGameStarted("OnGameStarted", FlowGame);
        OnGameFinished = new OnGameFinished("OnGameFinished", FlowGame);

        FlowGame.RunStateMachine(OnPreGameStarted);
    }
}
