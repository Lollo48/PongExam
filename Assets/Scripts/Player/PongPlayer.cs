using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class PongPlayer : NetworkBehaviour
{


    public static Action OnUpdateInformation;

    #region Server


    #endregion

    #region Client


    //client quando si connette viene chiamata su tutti i player in scena 
    public override void OnStartClient()
    {

        if (!isClientOnly) return;
        ((PongNetworkManager)NetworkManager.singleton).PongPlayers.Add(this);
        OnUpdateInformation?.Invoke();

    }


    public override void OnStopClient()
    {
        
        if (!isClientOnly) return;
        ((PongNetworkManager)NetworkManager.singleton).PongPlayers.Remove(this);
        OnUpdateInformation?.Invoke();

        PongNetworkManager.OnServerDisconnected?.Invoke();


    }
    #endregion



}
