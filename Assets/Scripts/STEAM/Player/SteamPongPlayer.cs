using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class SteamPongPlayer : NetworkBehaviour
{

    private string _displayName;

    #region Server


    #endregion


    #region Client
    public override void OnStartClient()
    {
        if (!isClientOnly) return;
        ((SteamPongNetworkManager)NetworkManager.singleton).CurrentPlayers.Add(this);

    }

    public override void OnStopClient()
    {

        if (!isClientOnly) return;
         
        ((SteamPongNetworkManager)NetworkManager.singleton).CurrentPlayers.Remove(this);

    }
    #endregion



}
