using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Steamworks;

public class SteamPongPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(UpdatePlayerName))]
    private string _playerName;

    public static Action OnUpdateInformation;

    public string GetName()
    {
        return _playerName;
    }

    #region Client

    public override void OnStartClient()
    {

        if (!isClientOnly) return;
        ((SteamPongNetworkManager)NetworkManager.singleton).CurrentPlayers.Add(this);

        Debug.Log("CLIENT PARTITO");
        CmdUpdateName();

    }

    public override void OnStopClient()
    {
        if (!isClientOnly) return;
        ((SteamPongNetworkManager)NetworkManager.singleton).CurrentPlayers.Remove(this);
        SteamPongNetworkManager.OnServerDisconnected?.Invoke(true);

        _playerName = "Waiting for players...";
        
    }

    #endregion


    [Command]
    public void CmdUpdateName()
    { 
        _playerName = SteamFriends.GetPersonaName();
    }


    //[ClientRpc]
    //public void RpcUpdatePlayerName()
    //{
    //    UpdatePlayerName();
    //}

    private void UpdatePlayerName(string oldName, string newName) => OnUpdateInformation?.Invoke();


}
