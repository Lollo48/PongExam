using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _matchMakingMenu;
    [SerializeField] private GameObject _lobbyMenu;

    private void Start()
    {
        MainMenuPanel(true);
    }

    private void OnEnable()
    {
        SteamPongNetworkManager.OnServerDisconnected += MainMenuPanel;
    }

    public void MainMenuPanel(bool isActive) => _mainMenu.SetActive(isActive);

    public void MatchMakingMenuPanel(bool isActive) => _matchMakingMenu.SetActive(isActive);

    public void LobbyMenuPanel(bool isActive) => _lobbyMenu.SetActive(isActive);

    public void QuitGame() => Application.Quit();

}
