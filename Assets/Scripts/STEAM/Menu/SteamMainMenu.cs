using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class SteamMainMenu : MonoBehaviour
{
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _joinLobbyButton;



    [SerializeField] private SteamMenuManager _menuManager;


    private void OnEnable()
    {
        _hostButton.onClick.AddListener(HostButton);
        _joinLobbyButton.onClick.AddListener(JoinButton);


    }


    private void OnDisable()
    {
        _hostButton.onClick.RemoveAllListeners();
        _joinLobbyButton.onClick.RemoveAllListeners();
    }


    private void HostButton()
    {
        ((SteamPongNetworkManager)NetworkManager.singleton).HostLobby();
        _menuManager.MainMenuPanel(false);
        _menuManager.MatchMakingMenuPanel(true);   
    }

    private void JoinButton()
    {
        ((SteamPongNetworkManager)NetworkManager.singleton).SearchLobby();
        _menuManager.MainMenuPanel(false);
        _menuManager.LobbyMenuPanel(true); 

    }


}
