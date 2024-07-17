using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _leftScore;
    [SerializeField] private int _rightScore;

    public static Action<FootballGoalPosition> OnScoreUpdate;

    public static event Action<int, int> OnScoreUpdated;

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

        //if(_leftScore == 10 || _rightScore == 10) //FlowGameManager.FlowGame.ChangeState(FlowGameManager.FlowGame.Contex.OnGameFinished);
        //else //FlowGameManager.FlowGame.ChangeState(FlowGameManager.FlowGame.Contex.OnPreGameStarted);

        UpdatedScore();
    }


    private void UpdatedScore() => OnScoreUpdated?.Invoke(_leftScore, _rightScore);



}


public enum FootballGoalPosition
{
    left,
    right
}