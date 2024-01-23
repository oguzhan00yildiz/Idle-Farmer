using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Connection : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button startButton;
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private string playerName;
    void Start()
    {
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
        startButton.onClick.AddListener(StartGame);

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    void Update()
    {
     
    }

    void Host()
    {
        NetworkManager.Singleton.StartHost();
        connectionPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    void Join()
    {
        NetworkManager.Singleton.StartClient();
        connectionPanel.SetActive(false);
        startPanel.SetActive(true);
    }

    void StartGame()
    {
        if (NetworkManager.Singleton.IsHost)
        {
            NetworkManager.Singleton.SceneManager.LoadScene("SampleScene",LoadSceneMode.Single);
        }
          
    }

    void OnClientConnected(ulong clientId)
    {
        // If we are server, say who connected and then stop
        if (IsHost)
        {
            Debug.Log(clientId + " connected");
            return;
        }

        // Client calls this
        ReceiveNameServerRpc(clientId, playerName);
    }

    void OnClientDisconnected(ulong clientId)
    {
        Debug.Log(clientId + " disconnected");
    }

    
    [ServerRpc(RequireOwnership = false)]
    void ReceiveNameServerRpc(ulong clientId, string name)
    {
        Debug.Log("Received " + name + " from " + clientId);

        // Now we are server and we call this
        // and it will run on all clients
        TellMeHisNameClientRpc(clientId, name);
    }

    // Runs on client(s) but is called by server
    [ClientRpc]
    void TellMeHisNameClientRpc(ulong clientId, string name)
    {
        Debug.Log("Client " + clientId + "'s name is " + name);
    }

}
