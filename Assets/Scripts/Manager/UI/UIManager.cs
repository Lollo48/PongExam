using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI[] _scoreText = new TextMeshProUGUI[2];

    private void Awake()
    {
        foreach(TextMeshProUGUI _scoreText in _scoreText)
        {
            _scoreText.text = "0";
        }
    }

    private void OnEnable()
    {
        OnPreGameStarted.OnPreGameUpdate += UpdateTimerText;
        OnPreGameStarted.OnPreGameEnd += DisableTimerText;
        OnPreGameStarted.OnPreGameStart += EnableTimerText;

        ScoreManager.OnScoreUpdated += UpdateScore;
    }

    private void OnDisable()
    {
        OnPreGameStarted.OnPreGameUpdate -= UpdateTimerText;
        OnPreGameStarted.OnPreGameEnd -= DisableTimerText;
        OnPreGameStarted.OnPreGameStart -= EnableTimerText;

        ScoreManager.OnScoreUpdated -= UpdateScore;
    }



    private void UpdateTimerText(float Timer) => _timer.text = Timer.ToString("0");
    private void DisableTimerText() => _timer.gameObject.SetActive(false);
    private void EnableTimerText() => _timer.gameObject.SetActive(true);


    private void UpdateScore(int Left,int Right)
    {
        _scoreText[0].text = Left.ToString();
        _scoreText[1].text = Right.ToString();
    }


}
