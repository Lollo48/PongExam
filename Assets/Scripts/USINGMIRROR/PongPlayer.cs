using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;


public class PongPlayer : NetworkBehaviour
{

    [SyncVar(hook = nameof(UpdateInformation))]
    private string _playerName;

    public static Action OnUpdateInformation;


    public string GetName()
    {
        return _playerName;
    }


    #region Server

    [Server]
    public void SetDisplayName(string name)
    {
        _playerName = name;
    }

    #endregion
    #region Client


    public override void OnStartClient()
    {

        if (!isClientOnly) return;
        ((PongNetworkManager)NetworkManager.singleton).PongPlayers.Add(this);

    }


    public override void OnStopClient()
    {
        OnUpdateInformation?.Invoke();

        if (!isClientOnly) return;

        Debug.Log("sto uscendo");

        ((PongNetworkManager)NetworkManager.singleton).PongPlayers.Remove(this);

        PongNetworkManager.OnServerDisconnected?.Invoke();

    }



    #endregion


    private void UpdateInformation(string oldName, string newName) => OnUpdateInformation?.Invoke();




}
