using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;


public class SteamPongNetworkManager : NetworkManager
{
    [HideInInspector] public List<SteamPongPlayer> CurrentPlayers;

    private static event Action<List<LobbyData>> OnLobbyesFound;
    private static event Action OnLobbyesNotFound;

    public override void OnStartServer()
    {
        CurrentPlayers = new List<SteamPongPlayer>();
    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        SteamPongPlayer player = conn.identity.GetComponent<SteamPongPlayer>();
        CurrentPlayers.Add(player);


        Debug.Log(conn.identity.netId);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        
        SteamPongPlayer player = conn.identity.GetComponent<SteamPongPlayer>();
        CurrentPlayers.Remove(player);

        base.OnServerDisconnect(conn);
    }


    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, maxConnections);
        
    }


    public void SearchLobby()
    {
        List<LobbyData> lobbies = new List<LobbyData>();

        for (int i = 0; i < SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagImmediate); i++)
        {
             // prendo numero amici su steam
             CSteamID friend = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagImmediate); //restituisce steam id dei miei amici
             
            if(SteamFriends.GetFriendGamePlayed(friend, out FriendGameInfo_t friendGameInfo_T)) // mi ritorna i giocatori che stanno giocando ad un gioco
            {
                if(friendGameInfo_T.m_gameID.ToString() == 480.ToString() &&
                    friendGameInfo_T.m_steamIDLobby.IsValid() &&
                    friendGameInfo_T.m_steamIDLobby.IsLobby())
                {
                    LobbyData newLobby = new LobbyData(friendGameInfo_T.m_steamIDLobby, SteamFriends.GetFriendPersonaName(friend));
                    lobbies.Add(newLobby); //tutte le lobbie valide che ho trovato
                }
            }

        }

        if (lobbies.Count > 0) OnLobbyesFound?.Invoke(lobbies);
        else OnLobbyesNotFound?.Invoke();

    }



}


public struct LobbyData
{
    private CSteamID CSteamID;
    private string _lobbyName;

    public LobbyData(CSteamID cSteamID, string lobbyName)
    {
        CSteamID = cSteamID;
        _lobbyName = lobbyName;
    }
}
