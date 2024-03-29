using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Connection : NetworkBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private Button startButton;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject connectionPanel;
    [SerializeField] private GameObject loadingPanel;

    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private GameObject chatPanel;

    public string playerName;
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
        playerName= nameInputField.text;
        if (nameInputField.isFocused)
        {
            nameInputField.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            
        }
        
    }

    void Host()
    {
        NetworkManager.Singleton.StartHost();
        connectionPanel.SetActive(false);
        startPanel.SetActive(true);
        chatPanel.SetActive(true);
    }

    void Join()
    {
        NetworkManager.Singleton.StartClient();
        connectionPanel.SetActive(false);
        startPanel.SetActive(false) ;
        loadingPanel.SetActive(true);   
        chatPanel.SetActive(true);
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
        if (IsHost)
        {
            Debug.Log(clientId + " connected");
            return;
        }

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
        TellMeHisNameClientRpc(clientId, name);
    }

    
    [ClientRpc]
    void TellMeHisNameClientRpc(ulong clientId, string name)
    {
        Debug.Log("Client " + clientId + "'s name is " + name);
    }

}
