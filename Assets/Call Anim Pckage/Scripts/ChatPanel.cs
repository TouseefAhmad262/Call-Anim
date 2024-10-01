using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatPanel : MonoBehaviour
{
    public GameObject MessageMainPanel;
    public GameObject MessagePrefeb;
    public GameObject DateLinePrefeb;
    public Transform MessagesParent;
    public GameObject LastDateLine;
    public TextMeshProUGUI ChatNameText;
    [HideInInspector]
    public MessagePrefeb ChatPrefrb;
    public TMP_InputField ChatInputField;

    public static string ChatName;
    public static List<MessageData> AllChatData;
    [HideInInspector]
    public bool CanHide;
    [HideInInspector]
    public static string S_Number;
    public string Number;

    [Header("<color=yellow>Others</color>")]
    public ContactsHandler contactsHandler;
    public GameObject TypingText;
    // Start is called before the first frame update

    void OnEnable()
    {
        Number = S_Number;
    }
    void Start()
    {
        Refresh();
        CanHide = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanHide)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                MessageMainPanel.SetActive(true);
                CloseChat();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SentMassage();
        }
        if(MessagesParent.GetChild(0).gameObject.transform.tag != "DateLine")
        {
            if(AllChatData.Count > 0)
            {
                CreateDateLine(AllChatData[0].Date, true);
            }
        }
    }
    public void Refresh()
    {
        ClearAllMesages();
        ChatNameText.text = ChatName;
        for (int i = 0; i < AllChatData.Count; i++)
        {
            if(LastDateLine != null)
            {
                if(LastDateLine.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text != AllChatData[i].Date)
                {
                    CreateDateLine(AllChatData[i].Date);
                }
            }
            else
            {
                CreateDateLine(AllChatData[i].Date);
            }
            GameObject Message = Instantiate(MessagePrefeb, MessagesParent);
            if (AllChatData[i].ByMe)
            {
                Message.gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperRight;
                Message.gameObject.GetComponent<HorizontalLayoutGroup>().reverseArrangement = true;
                Message.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(0, 161, 135, 255);
                Message.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
                Message.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(255, 255, 255, 255);
            }
            else
            {
                Message.gameObject.GetComponent<HorizontalLayoutGroup>().childAlignment = TextAnchor.UpperLeft;
                Message.gameObject.GetComponent<HorizontalLayoutGroup>().reverseArrangement = false;
                Message.gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                Message.gameObject.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(20, 20, 20, 255);
                Message.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().color = new Color32(20, 20, 20, 255);
            }

            Message.GetComponent<MessageBox>().MesageText.text = AllChatData[i].Message;
            Message.GetComponent<MessageBox>().TimeText.text = AllChatData[i].Time;
        }
    }

    void CreateDateLine(string Date, bool SetAsfirstSibling = false)
    {
        GameObject dateLine = Instantiate(DateLinePrefeb, MessagesParent);
        dateLine.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = Date;
        LastDateLine = dateLine;
        if(SetAsfirstSibling)
        {
            dateLine.transform.SetAsFirstSibling();
        }
    }

    public void ClearAllMesages()
    {
        for (int i = 0; i < MessagesParent.childCount; i++)
        {
            Destroy(MessagesParent.GetChild(i).gameObject);
        }
    }

    void HideIt()
    {
        transform.gameObject.SetActive(false);
    }

    public void CloseChat()
    {
        transform.GetComponent<Animator>().SetTrigger("Close");
    }

    void HideMainPanel()
    {
        MessageMainPanel.gameObject.SetActive(false);
        CanHide = true;
    }
    public void SentMassage()
    {
        if(ChatInputField.text != "")
        {
            MessageData data = new MessageData();
            data.Message = ChatInputField.text;
            data.ByMe = true;
            data.Date = DateTime.Now.ToString("dddd dd, MMMM");
            data.Time = ContactsHandler.CurrunTime;
            ChatPrefrb.AllChat.Add(data);
            AllChatData = ChatPrefrb.AllChat;
            ChatInputField.text = "";
            if(contactsHandler != null)
            {
                if (contactsHandler.Reply)
                {
                    TypingText.SetActive(true);
                }
            }
            Refresh();
        }
    }
}