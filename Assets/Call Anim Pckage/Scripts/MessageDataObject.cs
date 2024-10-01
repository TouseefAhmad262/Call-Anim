using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class MessageDataObject : MonoBehaviour
{
    public TMP_Dropdown Contact;
    public TMP_InputField Message;
    public TMP_InputField Time;
    public TMP_InputField Date;
    public Toggle Byme;

    void Awake()
    {
        Contact.ClearOptions();
        Contact.AddOptions(MessagesSetup.S_Names);
    }
}
