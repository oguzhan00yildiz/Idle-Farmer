using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode;

public class Chat : NetworkBehaviour
{
    public static Chat singleton;
    [SerializeField] private ChatMessage chatMessagePrefab;
    [SerializeField] private TMP_InputField chatInput;
    [SerializeField] private Connection connectionManager;

    [SerializeField] private string playerName;

    private void Awake()
    {
        Chat.singleton = this;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage(chatInput.text, playerName);
            chatInput.text = "";
                
        }

        playerName = connectionManager.playerName;
    }
    public void SendChatMessage(string message, string playerName)
    {
        string S = playerName +" > " + message;
        SendChatMessageServerRpc(S);  
    }

    void  AddMessage(string message)
    {
        chatMessagePrefab.SetText(message);
        
    }

    [ServerRpc(RequireOwnership = false)]
    void SendChatMessageServerRpc(string message)
    {
        ReceiveChatMessageClientRpc(message);
    }


    [ClientRpc]
    void ReceiveChatMessageClientRpc(string message)
    {
        Chat.singleton.AddMessage(message);
    }
}
