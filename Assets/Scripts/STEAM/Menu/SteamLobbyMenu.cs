using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using TMPro;

public class SteamLobbyMenu : MonoBehaviour
{
    [SerializeField] private GameObject _lobbySection;
    [SerializeField] private TextMeshProUGUI _noLobbyes;
    [SerializeField] private Button _refreshList;
    [SerializeField] private Button _TurnBackToMainMenu;

    [SerializeField] private SteamLobbySlot _lobbySlot;

    [SerializeField] private SteamMenuManager _menuManager;


    private void OnEnable()
    {
        SteamPongNetworkManager.OnLobbyesFound += OnLobbyListFound;
        SteamPongNetworkManager.OnLobbyesNotFound += OnlobbyListNotFound;
        _refreshList.onClick.AddListener(RefreshLobbyes);
        _TurnBackToMainMenu.onClick.AddListener(TurnBackToMainMenu);
    }


    private void OnDisable()
    {
        SteamPongNetworkManager.OnLobbyesFound -= OnLobbyListFound;
        SteamPongNetworkManager.OnLobbyesNotFound -= OnlobbyListNotFound;
        _refreshList.onClick.RemoveAllListeners();
        _TurnBackToMainMenu.onClick.RemoveAllListeners();
    }

    private void OnLobbyListFound(List<LobbyData> lobbyes)
    {
        Destroyer();
        _noLobbyes.gameObject.SetActive(false);
        InitializeLobbyList(lobbyes);
    }

    private void OnlobbyListNotFound()
    {
      
        _noLobbyes.gameObject.SetActive(true);

    }


    private void InitializeLobbyList(List<LobbyData> lobbyes)
    {
        foreach(LobbyData lobbyData in lobbyes)
        {
            SteamLobbySlot lobbySlot = Instantiate(_lobbySlot, _lobbySection.transform);
            lobbySlot.Inizialize(lobbyData);
        }
    }


    private void Destroyer()
    {
        List<Transform> currentLobbySpawned = new List<Transform>();
        for(int i = 0; i < _lobbySection.transform.childCount; i++)
        {
            currentLobbySpawned.Add(_lobbySection.transform.GetChild(i));
        }

        foreach(Transform lobby in currentLobbySpawned)
        {
            Destroy(lobby.gameObject);
        }

    }


    private void RefreshLobbyes() => ((SteamPongNetworkManager)NetworkManager.singleton).SearchLobby();

    private void TurnBackToMainMenu()
    {
        _menuManager.LobbyMenuPanel(false);
        _menuManager.MainMenuPanel(true); 
    }
}
