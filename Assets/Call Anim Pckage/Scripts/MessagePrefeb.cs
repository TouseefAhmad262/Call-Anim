using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrefeb : MonoBehaviour
{
    public string Name;
    public string Number;
    public List<MessageData> AllChat;


    [Space(20)]
    public Image Avatar;
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI FirstLaterText;
    public TextMeshProUGUI LastMessgeText;
    public TextMeshProUGUI LastMessgeDateText;
    // Start is called before the first frame update
    void Awake()
    {
        Avatar.color = new Color(Random.Range(0.4f, 0.85f), Random.Range(0.4f, 0.85f), Random.Range(0.4f, 0.85f), 255);
        FirstLaterText.text = Name[0].ToString();
        NameTxt.text = Name;
        LastMessgeText.text = AllChat[AllChat.Count - 1].Message;
        LastMessgeDateText.text = AllChat[AllChat.Count - 1].Date;
        gameObject.GetComponent<Button>().onClick.AddListener(OnContactClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnContactClick()
    {
        ChatPanel.AllChatData = AllChat;
        ChatPanel.ChatName = Name;
        ChatPanel.S_Number = Number;
        if(!MessageApp.S_ChatPanel.activeSelf)
        {
            MessageApp.S_ChatPanel.SetActive(true);
        }
        MessageApp.S_ChatPanel.gameObject.GetComponent<ChatPanel>().Refresh();
        MessageApp.S_ChatPanel.gameObject.GetComponent<ChatPanel>().ChatPrefrb = this;
        MessageApp.S_ChatPanel.gameObject.GetComponent<ChatPanel>().CanHide = false;
    }
}
[System.Serializable]
public class MessageData
{
    public string Message;
    public bool ByMe;
    public string Time;
    public string Date;
}