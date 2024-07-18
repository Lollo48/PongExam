using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;


public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuPanel;

    private void OnEnable()
    {
        PongNetworkManager.OnServerDisconnected += SetMenuPanelActive;
        
    }

    private void OnDisable()
    {
        PongNetworkManager.OnServerDisconnected -= SetMenuPanelActive;
    }
    public void HostLobby()
    {
        NetworkManager.singleton.StartHost();
    }


    private void SetMenuPanelActive() => MainMenuPanel.SetActive(true);

    


    
}
