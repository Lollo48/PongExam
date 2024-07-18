using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ScoreManager : NetworkBehaviour
{
    [SyncVar (hook = nameof (UpdatedScore))]
    private int _leftScore;

    [SyncVar(hook = nameof(UpdatedScore))]
    private int _rightScore;

    [SyncVar(hook = nameof (SetWinner))]
    private int _winner;


    public static Action<FootballGoalPosition> OnScoreUpdate;

    public static event Action<int, int> OnScoreUpdated;

    public static event Action<int,int> OnSetWinner;


    private void OnEnable()
    {
        OnScoreUpdate += UpdateScore;
    }

    private void OnDisable()
    {
        OnScoreUpdate -= UpdateScore;
    }

    public void UpdateScore(FootballGoalPosition goalPosition)
    {
       

        if (goalPosition == FootballGoalPosition.left) _leftScore++;
        else _rightScore++;

        if (_leftScore == 5)
        {
            _winner = 2;
            GameManager.Instance.flowGame.FlowGame.ChangeState(GameManager.Instance.flowGame.FlowGame.Contex.OnGameFinished);
        }
        else if( _rightScore == 5)
        {
            _winner = 1;
            GameManager.Instance.flowGame.FlowGame.ChangeState(GameManager.Instance.flowGame.FlowGame.Contex.OnGameFinished);
        }
        else
        {
            GameManager.Instance.flowGame.FlowGame.ChangeState(GameManager.Instance.flowGame.FlowGame.Contex.OnGameStarted); 
        }
    }

   
    private void UpdatedScore(int a, int b) => OnScoreUpdated?.Invoke(_leftScore, _rightScore);

    private void SetWinner(int a, int b) => OnSetWinner?.Invoke(_winner,default);


}


public enum FootballGoalPosition
{
    left,
    right
}