using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using System;


public class SteamPongNetworkManager : NetworkManager
{
    [HideInInspector] public List<SteamPongPlayer> CurrentPlayers;

    GameObject ball;

    public static event Action<List<LobbyData>> OnLobbyesFound;
    public static event Action OnLobbyesNotFound;

    public static event Action OnGameCanStart;
    public static Action<bool> OnServerDisconnected;


    private void OnEnable()
    {
        OnGameStarted.OnGameStart += BallSpawn;
    }
    public override void OnStartServer()
    {
        CurrentPlayers = new List<SteamPongPlayer>();
    }


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        SteamPongPlayer player = conn.identity.GetComponent<SteamPongPlayer>();
        CurrentPlayers.Add(player);

        player.CmdUpdateName();

        if (CurrentPlayers.Count >= 2)
            OnGameCanStart?.Invoke();
        //Debug.Log(conn.identity.netId);
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        
        SteamPongPlayer player = conn.identity.GetComponent<SteamPongPlayer>();
        CurrentPlayers.Remove(player);

        //OnServerDisconnected?.Invoke(true);


        base.OnServerDisconnect(conn);
    }


    public void HostLobby()
    {
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, maxConnections);
        //Debug.Log("LOBBY CREATA");
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


    #region Ball 
    [Server]
    private void BallSpawn()
    {
        ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        NetworkServer.Spawn(ball);
    }

    public void DestroyBall() => Destroy(ball);


    [Server]
    public void ResetBallPosition()
    {
        ball.SetActive(false);
        ball.transform.position = new Vector3(0f, BallRandomPosition(), 0f);
        ball.SetActive(true);
    }


    private float BallRandomPosition()
    {
        float c = 0;
        return c = UnityEngine.Random.Range(-7, 9);
    }

    #endregion

}


