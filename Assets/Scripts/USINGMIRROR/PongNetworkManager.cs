using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PongNetworkManager : NetworkManager
{
    public List<PongPlayer> PongPlayers;

    GameObject ball;

    public static Action OnServerDisconnected;

    public override void Awake()
    {
        base.Awake();
        networkAddress = "localhost";
    }

    public void OnEnable()
    {
        OnGameStarted.OnGameStart += BallSpawn;
    }

    private void OnDisable()
    {
        OnGameStarted.OnGameStart -= BallSpawn;
    }


    #region Server
    public override void OnStartServer()
    {
        base.OnStartServer();
        PongPlayers =  new List<PongPlayer>();
        
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        PongPlayer pongPlayer = conn.identity.GetComponent<PongPlayer>();
        PongPlayers.Add(pongPlayer);

        PongPlayer.OnUpdateInformation?.Invoke();

    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PongPlayer player = conn.identity.GetComponent<PongPlayer>();
        PongPlayers.Remove(player);

        PongPlayer.OnUpdateInformation?.Invoke();

        OnServerDisconnected?.Invoke();

        base.OnServerDisconnect(conn);
    }

    #endregion


    #region Client

    public override void OnStopClient()
    {
        PongPlayers.Clear();
    }

    #endregion


    
    private void BallSpawn()
    {
        ball = Instantiate(spawnPrefabs.Find(prefab => prefab.name == "Ball"));
        NetworkServer.Spawn(ball);
    }

    public void DestroyBall() => Destroy(ball);

}
