using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class AllStruct : MonoBehaviour
{
    
}



public struct LobbyData
{
    public CSteamID CSteamID;
    public string LobbyName;

    public LobbyData(CSteamID cSteamID, string lobbyName)
    {
        CSteamID = cSteamID;
        LobbyName = lobbyName;
        
    }

    public bool IsValid()
    {
        if (CSteamID.IsValid())
            return true;
        else return false;
    }
}