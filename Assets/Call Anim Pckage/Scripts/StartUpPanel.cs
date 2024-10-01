using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartUpPanel : MonoBehaviour
{
    #region Values
    public TMP_Dropdown AppToRunDropdown;
    public TMP_Dropdown ModeDropdown;
    public TMP_Dropdown ScreenModeDropdown;
    public TMP_InputField incomingCallTimeField;
    public TMP_InputField Message;
    public TMP_InputField ReplyMessage;
    public Toggle RandomCallToggle;
    public Toggle ReplyToggle;
    public TMP_InputField CallerName;
    public TMP_InputField CallerNumber;
    public TMP_Dropdown TimeType;
    public TMP_InputField CustomTimeMin;
    public TMP_InputField CustomTimeSec;

    [Space(10)]

    public GameObject ToggleObject;
    public GameObject NameAndNumberObject;
    public GameObject CustomTimeObject;

    public GameObject Startingpanel;
    public Toggle WIFI_Toggle;

    #endregion
    #region Data
    public int AppToRun;
    public int Mode;
    public int ScreenMode;
    public float IncomingCallTime;
    public bool RandomCall;
    public bool Reply;
    public string CallName;
    public string CallNumber;
    public int Timetype;
    public string CustomMin;
    public string CustomSec;
    public ContactsHandler contactsHandler;
    public static bool WIFI;

    public Sprite Main_BG;
    #endregion
    [Header("<color=yellow>BackGround Setup</color>")]
    public List<Sprite> Backgrounds;
    public GameObject MainBGPanel;
    public GameObject BackgroundPrefeb;
    public Transform BackgroundsParent;
    public string CSVPath = "Assets/Call Anim Pckage/CSV's/data.csv";
    public TMP_InputField CSVPathField;
    public List<prebuildMessages> PrebuildMessages;
    public List<string> VideoPaths;
    public Slider LoadimeSlider;
    public float ThumnailLoadTime;
    public static bool WasGame;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Backgrounds.Count; i++)
        {
            InstentiateBG(Backgrounds[i]);
        }
        UpdateLoadTimeValue();
    }
    public void InstentiateBG(Sprite BG_Image, bool SetAsLast = false)
    {
        GameObject BG = Instantiate(BackgroundPrefeb, BackgroundsParent);
        if (!SetAsLast)
        {
            BG.transform.SetAsFirstSibling();
        }
        else
        {
            BG.transform.SetSiblingIndex(BackgroundsParent.childCount - 2);
        }
        BG.GetComponent<Image>().sprite = BG_Image;
        BG.AddComponent<Button>().onClick.AddListener(OnBgClick);
    }
    public void OnBgClick()
    {
        MainBGPanel.GetComponent<Image>().sprite = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;
        Main_BG = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite;

    }
    public bool DataSent;
    // Update is called once per frame
    void Update()
    {
        WIFI = WIFI_Toggle.isOn;
        if(SceneManager.GetActiveScene().name == "Call Anim Scene")
        {
            //SendAllData();
        }
        else if(SceneManager.GetActiveScene().name == "Start Scene")
        {
            ValuesCheck();
        }
        if (!DataSent)
        {
            CSVPathField.text = CSVPath;
        }
    }
    public void UpdateLoadTimeValue()
    {
        ThumnailLoadTime = LoadimeSlider.value;
    }
    void ValuesCheck()
    {
        if(AppToRunDropdown.value == 0)
        {
            if (ModeDropdown.value == 1)
            {
                ToggleObject.SetActive(true);
                if (!RandomCallToggle.isOn)
                {
                    NameAndNumberObject.SetActive(true);
                }
                else
                {
                    NameAndNumberObject.SetActive(false);
                }
            }
            else
            {
                ToggleObject.SetActive(false);
                NameAndNumberObject.SetActive(false);
            }

            ModeDropdown.transform.parent.gameObject.SetActive(true);
            incomingCallTimeField.transform.parent.gameObject.SetActive(true);
            Message.transform.parent.gameObject.SetActive(false);
            ReplyToggle.gameObject.transform.parent.gameObject.SetActive(false);
            ReplyMessage.gameObject.transform.parent.gameObject.SetActive(false);
        }
        else if(AppToRunDropdown.value == 1)
        {
            ModeDropdown.transform.parent.gameObject.SetActive(true);
            if(ModeDropdown.value == 0)
            {
                incomingCallTimeField.transform.parent.gameObject.SetActive(false);
                ToggleObject.SetActive(false);
                Message.transform.parent.gameObject.SetActive(false);
            }
            else if(ModeDropdown.value == 1)
            {
                incomingCallTimeField.transform.parent.gameObject.SetActive(true);
                ToggleObject.SetActive(true);
                Message.transform.parent.gameObject.SetActive(true);
                if (RandomCallToggle.isOn)
                {
                    NameAndNumberObject.SetActive(false);
                }
                else
                {
                    NameAndNumberObject.SetActive(true);
                }
            }
            ReplyToggle.gameObject.transform.parent.gameObject.SetActive(true);
            if (ReplyToggle.isOn)
            {
                ReplyMessage.gameObject.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                ReplyMessage.gameObject.transform.parent.gameObject.SetActive(false);
            }
        }

        if (TimeType.value == 1)
        {
            CustomTimeObject.SetActive(true);
        }
        else
        {
            CustomTimeObject.SetActive(false);
        }
    }
    void CollectAllData()
    {
        AppToRun = AppToRunDropdown.value;
        Mode = ModeDropdown.value;
        ScreenMode = ScreenModeDropdown.value;
        float.TryParse(incomingCallTimeField.text, out IncomingCallTime);
        RandomCall = RandomCallToggle.isOn;
        Reply = ReplyToggle.isOn;
        CallName = CallerName.text;
        CallNumber = CallerNumber.text;
        Timetype = TimeType.value;
        CustomMin = CustomTimeMin.text;
        CustomSec = CustomTimeSec.text;
        for (int i = 0; i < GetComponent<MessagesSetup>().Messages.Count; i++)
        {
            prebuildMessages msg = new prebuildMessages();
            msg.ContactName = GetComponent<MessagesSetup>().Names[GetComponent<MessagesSetup>().Messages[i].Contact.value];
            msg.Number = GetComponent<MessagesSetup>().Numbers[GetComponent<MessagesSetup>().Messages[i].Contact.value];
            msg.Message = GetComponent<MessagesSetup>().Messages[i].Message.text;
            msg.Time = GetComponent<MessagesSetup>().Messages[i].Time.text;
            msg.Date = GetComponent<MessagesSetup>().Messages[i].Date.text;
            msg.Byme = GetComponent<MessagesSetup>().Messages[i].Byme.isOn;
            PrebuildMessages.Add(msg);
        }
    }
    public void SendAllData()
    {
        contactsHandler = GameObject.FindAnyObjectByType<ContactsHandler>();
        contactsHandler.VideosParent.VideoPaths = VideoPaths;
        contactsHandler.VideosParent.LoadTime = ThumnailLoadTime;
        if(Mode == 0)
        {
            contactsHandler.Mode = Modeselect.OnGoing;
        }else if(Mode == 1)
        {
            contactsHandler.Mode = Modeselect.InComing;
        }

        if(AppToRun == 0)
        {
            contactsHandler.App = ApptoRun.Calling;
        }
        else if(AppToRun == 1)
        {
            contactsHandler.App = ApptoRun.Message;
            contactsHandler.Message = Message.text;
            contactsHandler.Reply = Reply;
            contactsHandler.ReplyMessage = ReplyMessage.text;
        }

        contactsHandler.PrebuildMessages = PrebuildMessages;

        if(ScreenMode == 0)
        {
            contactsHandler.ScreenMode = ScreenOffCheck.OnScreen;
        }
        else if(ScreenMode == 1)
        {
            contactsHandler.ScreenMode = ScreenOffCheck.OffScreen;
        }

        contactsHandler.RandomCaller = RandomCall;
        contactsHandler.IncomingCallWaitTime = IncomingCallTime;
        contactsHandler.Name = CallName;
        contactsHandler.Number = CallNumber;

        if(Timetype == 0)
        {
            contactsHandler.TimeType = TimeMode.CurruntTime;
        }
        else if(Timetype == 1)
        {
            contactsHandler.TimeType = TimeMode.Custom;
        }

        contactsHandler.customTimeMin = CustomMin;
        contactsHandler.customTimeSec = CustomSec;

        contactsHandler.BackgroundPanel.GetComponent<Image>().sprite = Main_BG;
    }
    public void StartApp()
    {
        CollectAllData();
        Startingpanel.SetActive(true);
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Call Anim Scene");
    }
}
[System.Serializable]
public class prebuildMessages
{
    public string ContactName;
    public int Number;
    public string Message;
    public string Time;
    public string Date;
    public bool Byme;
}