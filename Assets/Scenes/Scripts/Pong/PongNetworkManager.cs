using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PongNetworkManager : NetworkManager
{
    GameObject _ball;

    public List<PongPlayer> PongPlayers { get; } = new List<PongPlayer>();


    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        PongPlayer pongPlayer = conn.identity.GetComponent<PongPlayer>();
        PongPlayers.Add(pongPlayer);


    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        PongPlayer player = conn.identity.GetComponent<PongPlayer>();
        PongPlayers.Remove(player);

        base.OnServerDisconnect(conn);
    }


}
