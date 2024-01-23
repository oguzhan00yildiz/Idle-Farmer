using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatMessage : MonoBehaviour  
{
    [SerializeField] GameObject messageTextPrefab;
    [SerializeField] Transform messageParentTransform;

    public void SetText(string str)
    {
        GameObject newMessageLine= Instantiate(messageTextPrefab, messageParentTransform);
        newMessageLine.transform.parent = messageParentTransform;
        newMessageLine.GetComponent<TextMeshProUGUI>().text = str;
    }

}
