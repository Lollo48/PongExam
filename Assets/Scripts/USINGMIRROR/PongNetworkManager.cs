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

        pongPlayer.SetDisplayName($"Player {PongPlayers.Count}");

        PongPlayer.OnUpdateInformation?.Invoke();

    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PongPlayer player = conn.identity.GetComponent<PongPlayer>();
        PongPlayers.Remove(player);

        PongPlayer.OnUpdateInformation?.Invoke();

        OnServerDisconnected?.Invoke();

        base.OnServerDisconnect(conn);


        if (ball == null) return;
            DestroyBall();
    }

    #endregion


    #region Client

    public override void OnStopClient()
    {
        PongPlayers.Clear(); 
    }

    #endregion

    public void RestartLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected) StopHost();
        else StopClient();

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
