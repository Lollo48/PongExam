using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;

public class SteamLobbyManager : MonoBehaviour
{
    private const string _HostAddresKey = "HostAddress";

    public static event Action<LobbyCreated_t> OnLobbyCreated;
    public static event Action<LobbyEnter_t> OnLobbyEntered;
    public static event Action<GameLobbyJoinRequested_t> OnGameLobbyJoinRequested;


    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;


    private void Start()
    {
        if (!SteamManager.Initialized) return;

        lobbyCreated = Callback<LobbyCreated_t>.Create(LobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(GameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(LobbyEntered);
    }


    // QUANDO ANDRò A CHIAMARE IL METODO CREATE LOBBY DI STEAMMATCHMAKING CALL BACK SU QUESTO
    private void LobbyCreated(LobbyCreated_t Callback)
    {

        if (Callback.m_eResult != EResult.k_EResultOK) return;

        NetworkManager.singleton.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(Callback.m_ulSteamIDLobby), _HostAddresKey, SteamUser.GetSteamID().ToString());

        OnLobbyCreated?.Invoke(Callback);
    }


    private void LobbyEntered(LobbyEnter_t Callback)
    {

        if (NetworkServer.active) return; 

        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(Callback.m_ulSteamIDLobby), _HostAddresKey);
        NetworkManager.singleton.networkAddress = hostAddress;
        NetworkManager.singleton.StartClient();

        OnLobbyEntered?.Invoke(Callback);

    }


    private void GameLobbyJoinRequested(GameLobbyJoinRequested_t Callback)
    {

        SteamMatchmaking.JoinLobby(Callback.m_steamIDLobby);

        OnGameLobbyJoinRequested?.Invoke(Callback);
    }
}
