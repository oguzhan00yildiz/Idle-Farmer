using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Unity.Netcode;
using UnityEngine.UI;

public class Connection : MonoBehaviour
{
    [SerializeField] private Button hostButton;
    [SerializeField] private Button joinButton;
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
}
