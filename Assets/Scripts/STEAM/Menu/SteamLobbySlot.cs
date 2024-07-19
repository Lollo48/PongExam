using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Steamworks;
using Mirror;

public class SteamLobbySlot : MonoBehaviour
{

    private LobbyData _lobbyData;
    [SerializeField] private TextMeshProUGUI _friendName;
    [SerializeField] private Button _joinLobbyButton;


    private SteamMenuManager _steamMenu;



    public void Inizialize(LobbyData data)
    {
        if (!data.IsValid())
        {
            Destroy(gameObject);
            return;
        }

        _lobbyData = data;
        _friendName.text = data.LobbyName;
        _joinLobbyButton.onClick.AddListener(JoinLobbyButtonClick);
        _steamMenu = FindObjectOfType<SteamMenuManager>();
    }

    private void JoinLobbyButtonClick()
    {
        SteamMatchmaking.JoinLobby(_lobbyData.CSteamID);

        //NetworkManager.singleton.StartClient();

        _steamMenu.LobbyMenuPanel(false);
        _steamMenu.MatchMakingMenuPanel(true);

    }



}
