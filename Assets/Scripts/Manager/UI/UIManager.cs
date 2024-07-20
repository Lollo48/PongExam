using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _timer;
    [SerializeField] private TextMeshProUGUI[] _scoreText = new TextMeshProUGUI[2];

    [SerializeField] private GameObject _turnBackToMainMenu;
    [SerializeField] private TextMeshProUGUI _winnerTest;
    [SerializeField] private Button _TurnBack;

    private void Awake()
    {
        foreach(TextMeshProUGUI _scoreText in _scoreText)
        {
            _scoreText.text = "0";
        }
    }

    private void OnEnable()
    {
        _turnBackToMainMenu.SetActive(false);

        OnPreGameStarted.OnPreGameUpdate += UpdateTimerText;
        OnPreGameStarted.OnPreGameEnd += DisableTimerText;
        OnPreGameStarted.OnPreGameStart += EnableTimerText;

        ScoreManager.OnScoreUpdated += UpdateScore;
        ScoreManager.OnSetWinner += SetWinner;

        _TurnBack.onClick.AddListener(ChangeScene);
    }

    private void OnDisable()
    {
        OnPreGameStarted.OnPreGameUpdate -= UpdateTimerText;
        OnPreGameStarted.OnPreGameEnd -= DisableTimerText;
        OnPreGameStarted.OnPreGameStart -= EnableTimerText;

        ScoreManager.OnScoreUpdated -= UpdateScore;
        ScoreManager.OnSetWinner -= SetWinner;

        _TurnBack.onClick.RemoveAllListeners();
    }



    private void UpdateTimerText(float Timer) => _timer.text = Timer.ToString("0");
    private void DisableTimerText() => _timer.gameObject.SetActive(false);
    private void EnableTimerText() => _timer.gameObject.SetActive(true);


    private void UpdateScore(int Left,int Right)
    {
        _scoreText[0].text = Left.ToString();
        _scoreText[1].text = Right.ToString();
    }


    private void SetWinner(int winner,int a)
    {
        _turnBackToMainMenu.SetActive(true);
        _winnerTest.text = ($"Player {winner } win !");

    }


    //Fix temporaneo non ho la possibilità di testare con qualcuno la fine del livello in questo momento (con la build senza steam funziona)
    private void ChangeScene()
    {

        if (NetworkServer.active && NetworkClient.isConnected) ((SteamPongNetworkManager)NetworkManager.singleton).StopHost();
        else ((SteamPongNetworkManager)NetworkManager.singleton).StopClient();
    }
}
