using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mirror;

public class LobbyMenu : NetworkBehaviour
{
    [SerializeField] private Button startGameButton = null;
    [SerializeField] private TMP_Text[] playerNameTexts = new TMP_Text[2];


    private void OnEnable()
    {
        PongPlayer.OnUpdateInformation += ClientHandleInfoUpdated;
    }

    private void OnDisable()
    {
        PongPlayer.OnUpdateInformation -= ClientHandleInfoUpdated;
    }

    private void ClientHandleInfoUpdated()
    {
        List<PongPlayer> players = ((PongNetworkManager)NetworkManager.singleton).PongPlayers;
        

        for (int i = 0; i < players.Count; i++)
        {
            playerNameTexts[i].text = ($"Player {i + 1}");
        }

        for (int i = players.Count; i < playerNameTexts.Length; i++)
        {
            playerNameTexts[i].text = "Waiting For Player...";
        }

        if (players.Count >= 2)
            startGameButton.interactable = true;
    }

    public void StartGame()
    {
        
        NetworkManager.singleton.ServerChangeScene("GameScene");
    }

    public void LeaveLobby()
    {
        if (NetworkServer.active && NetworkClient.isConnected)
        {
            NetworkManager.singleton.StopHost();
        }
        else
        {
            NetworkManager.singleton.StopClient();
            

        }
    }


    public void StartClient() => NetworkManager.singleton.StartClient();

}
