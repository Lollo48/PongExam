using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;
using System;

public class SteamMatchMakingMenu : MonoBehaviour
{
    [SerializeField] private Button _startGameButton;
    [SerializeField] private Button _quitMatchMakingButton;

    [SerializeField] private TextMeshProUGUI[] _playerName = new TextMeshProUGUI[2];

    [SerializeField] private SteamMenuManager _menuManager;


    private void OnEnable()
    {
        SteamPongNetworkManager.OnGameCanStart += CanStartGame;
        SteamPongPlayer.OnUpdateInformation += UpdatePlayerName;
        _startGameButton.interactable = false;
        _startGameButton.onClick.AddListener(ChangeScene);
        _quitMatchMakingButton.onClick.AddListener(LeaveLobby);

    }

    private void OnDisable()
    {
        _startGameButton.onClick.RemoveAllListeners();
        _quitMatchMakingButton.onClick.RemoveAllListeners();
        SteamPongNetworkManager.OnGameCanStart -= CanStartGame;
        SteamPongPlayer.OnUpdateInformation -= UpdatePlayerName;
    }

    private void UpdatePlayerName()
    {
        List<CSteamID> CSteamIDs = SteamLobbyManager.GetAllPlayerInLobby();
        //Debug.Log(CSteamIDs.Count);
        foreach (CSteamID player in CSteamIDs)
        {
            int i = 0;
            string friendName = SteamFriends.GetFriendPersonaName(player);
            _playerName[i].text = friendName.ToString();
            i++;
        }

        //List<SteamPongPlayer> steamPongPlayers = ((SteamPongNetworkManager)NetworkManager.singleton).CurrentPlayers;

        //foreach (SteamPongPlayer steamPongPlayer in steamPongPlayers)
        //{
        //    int i = 0;
        //    _playerName[i].text = steamPongPlayer.GetName().ToString();
        //    i++;
        //}



    }


    [Server]
    private void ChangeScene() => NetworkManager.singleton.ServerChangeScene("GameScene");

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected) NetworkManager.singleton.StopHost();
        else if (NetworkClient.isConnected) NetworkManager.singleton.StopClient();
        else if (NetworkServer.active) NetworkManager.singleton.StopServer();

        _menuManager.MatchMakingMenuPanel(false);
        _menuManager.MainMenuPanel(true);
 
    }


    private void CanStartGame() => _startGameButton.interactable = true;

}
