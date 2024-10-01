using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typing : MonoBehaviour
{
    public GameObject ScriptHandler;
    ContactsHandler contactsHandler;
    MessageApp messageApp;
    void Awake()
    {
        messageApp = ScriptHandler.GetComponent<MessageApp>();
        contactsHandler = ScriptHandler.GetComponent<ContactsHandler>();
    }
    void AddMessage()
    {
        messageApp.GetMessage(ChatPanel.ChatName, ChatPanel.S_Number, contactsHandler.ReplyMessage);
        gameObject.SetActive(false);
    }
}