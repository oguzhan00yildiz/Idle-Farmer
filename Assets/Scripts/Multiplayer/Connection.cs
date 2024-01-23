using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using UnityEngine.UI;

public class Connection : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private string playerName;
    void Start()
    {
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    void Host()
    {
        NetworkManager.Singleton.StartHost();
        
    }

    void Join()
    {
        NetworkManager.Singleton.StartClient();
        
    }

    [ClientRpc]
    void RpcClientConnected()
    {
        RpcServerConnected();
    }

    [ServerRpc]
    void RpcServerConnected()
    {
        Debug.Log(playerName + " has connected to the server");
    }
}
