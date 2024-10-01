using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MessageApp : MonoBehaviour
{
    public static GameObject S_ChatPanel;
    public GameObject ChatPanelObject;
    public Notification NotificationPanel;
    public static Notification S_NotificationPanel;
    public GameObject MessageMainPanel;

    public Transform MessageContactsParent;
    public GameObject MessageContactPrefeb;

    bool OpeningChat;
    MessagePrefeb ToOpenChatPrefeb;

    // Start is called before the first frame update
    void Awake()
    {
        S_ChatPanel = ChatPanelObject;
        S_NotificationPanel = NotificationPanel;
    }

    // Update is called once per frame
    void Update()
    {
        if (OpeningChat)
        {
            if (MessageMainPanel.gameObject.GetComponent<MessageMainPanel>().IsOpened)
            {
                ToOpenChatPrefeb.gameObject.GetComponent<Button>().onClick.Invoke();
                OpeningChat = false;
            }
        }
    }

    public void GetMessage(string Name, string number ,string Message, bool customtime = false, string Date = "dddd dd, MMMM", string Time = "00:00", bool Notification = true, bool BYME = false)
    {
        List<MessageData> FinalData = new List<MessageData>();
        if (MessageContactsParent.childCount > 0)
        {
            for (int i = 0; i < MessageContactsParent.childCount; i++)
            {
                if (MessageContactsParent.GetChild(i).gameObject.GetComponent<MessagePrefeb>().Name == Name && MessageContactsParent.GetChild(i).gameObject.GetComponent<MessagePrefeb>().Number == number)
                {
                    MessageData data = new MessageData();
                    data.Message = Message;
                    if (!customtime)
                    {
                        data.ByMe = false;
                        data.Date = DateTime.Now.ToString("dddd dd, MMMM");
                        data.Time = ContactsHandler.CurrunTime;
                    }
                    else
                    {
                        data.ByMe = BYME;
                        data.Date = Date;
                        data.Time = Time;
                    }
                    MessageContactsParent.GetChild(i).gameObject.GetComponent<MessagePrefeb>().AllChat.Add(data);
                    FinalData = MessageContactsParent.GetChild(i).gameObject.GetComponent<MessagePrefeb>().AllChat;
                    ToOpenChatPrefeb = MessageContactsParent.GetChild(i).gameObject.GetComponent<MessagePrefeb>();
                    break;
                }
                else if (i == MessageContactsParent.childCount - 1)
                {
                    MessagePrefeb messagePrefeb = Instantiate(MessageContactPrefeb, MessageContactsParent).gameObject.GetComponent<MessagePrefeb>();
                    print(messagePrefeb);
                    messagePrefeb.Name = Name;
                    messagePrefeb.Number = number;
                    MessageData data = new MessageData();
                    data.Message = Message;
                    if (!customtime)
                    {
                        data.ByMe = false;
                        data.Date = DateTime.Now.ToString("dddd dd, MMMM");
                        data.Time = ContactsHandler.CurrunTime;
                    }
                    else
                    {
                        data.ByMe = BYME;
                        data.Date = Date;
                        data.Time = Time;
                    }
                    messagePrefeb.AllChat.Add(data);
                    FinalData = messagePrefeb.AllChat;
                    ToOpenChatPrefeb = messagePrefeb;
                }
            }
        }
        else
        {
            MessagePrefeb messagePrefeb = Instantiate(MessageContactPrefeb, MessageContactsParent).gameObject.GetComponent<MessagePrefeb>();
            print(messagePrefeb);
            messagePrefeb.Name = Name;
            messagePrefeb.Number = number;
            MessageData data = new MessageData();
            data.Message = Message;
            if (!customtime)
            {
                data.ByMe = false;
                data.Date = DateTime.Now.ToString("dddd dd, MMMM");
                data.Time = ContactsHandler.CurrunTime;
            }
            else
            {
                data.ByMe = BYME;
                data.Date = Date;
                data.Time = Time;
            }
            messagePrefeb.AllChat.Add(data);
            FinalData = messagePrefeb.AllChat;
            ToOpenChatPrefeb = messagePrefeb;
        }

        if (!ChatPanelObject.activeSelf)
        {
            if (Notification)
            {
                GetNotification(Name, Message, OnNotificationCick);
            }
        }
        else
        {
            if(ChatPanelObject.gameObject.GetComponent<ChatPanel>().ChatNameText.text == Name && ChatPanelObject.gameObject.GetComponent<ChatPanel>().Number == number)
            {
                ChatPanel.AllChatData = FinalData;
                ChatPanelObject.gameObject.GetComponent<ChatPanel>().Refresh();
            }
            else
            {
                if (Notification)
                {
                    GetNotification(Name, Message, OnNotificationCick);
                }
            }
        }
    }
    public void OnNotificationCick()
    {
        print("Notification hit!");
        if (!MessageMainPanel.activeSelf)
        {
            MessageMainPanel.SetActive(true);
            MessageMainPanel.gameObject.GetComponent<MessageMainPanel>().OpenIt();
            OpeningChat = true;
        }
        else
        {
            if (MessageMainPanel.gameObject.GetComponent<MessageMainPanel>().IsOpened)
            {
                print("Invoked");
                ToOpenChatPrefeb.gameObject.GetComponent<Button>().onClick.Invoke();
            }
        }
        S_NotificationPanel.HideNotificationEarlier();
    }

    public static void GetNotification(string Title, string Description, UnityAction OnClickMethod = null)
    {
        S_NotificationPanel.NotificationAnimator.gameObject.SetActive(true);
        S_NotificationPanel.Message.text = Description;
        S_NotificationPanel.Title.text = Title;
        if(OnClickMethod != null)
        {
            S_NotificationPanel.gameObject.GetComponent<Button>().onClick.AddListener(OnClickMethod);
        }
        else
        {
            S_NotificationPanel.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
}