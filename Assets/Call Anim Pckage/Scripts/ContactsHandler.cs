using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ContactsHandler : MonoBehaviour
{
    [Header("<color=orange>Contacts System")]
    public List<Contact_Data> Contacts;
    public static List<Contact_Data> S_Contacts;
    public string ContactsaPath;
    public Modeselect Mode;
    public ScreenOffCheck ScreenMode;
    public float IncomingCallWaitTime = 5;
    [Header("<color=Green> if Random Caller is true \n it will get a random contact for incoming call name and Number.\n other wise it will take name and number values below."), Tooltip("if RandomCaller is true it will get a random contact for incoming call name and Number. other wise it will take name and number values below.")]
    public bool RandomCaller;
    public string Name;
    public string Number;
    [Space(20), Header("<color=yellow>Time Details")]
    public TimeMode TimeType;
    public List<TextMeshProUGUI> TimeText;
    public TextMeshProUGUI MainTimeText;
    public TextMeshProUGUI MainDateText;

    public string customTimeMin;
    public string customTimeSec;

    public static string CurrunTime;

    [Space(20), Header("<color=yellow>Other's")]
    public Contact ContactPrefeb;
    public Portion PortionPrefeb;
    public Transform ContactParent;
    public static Transform ContactsParent;
    public GameObject callingPanel;
    public static GameObject S_CallingPanel;

    public GameObject ContactsPanel;
    public GameObject PhonePanel;
    public GameObject IncomingCall;
    public GameObject IncomingPanel;
    public string Message;
    public GameObject BlackScreenPanel;
    public TextMeshProUGUI CallerTimeText;
    public GameObject StartingAppPanel;
    public static bool IsCallStarted;
    public bool Reply;
    public string ReplyMessage;
    float CallTimer;

    public GameObject BackgroundPanel;
    public StartUpPanel startUpPanel;
    public GameObject VideosPanel;
    public TumbnailGenration VideosParent;

    public List<prebuildMessages> PrebuildMessages;

    [Space(20), Header("<color=purple>Apps Manager</color>")]
    public ApptoRun App;
    // Start is called before the first frame update
    void LoadCSV(string filePath)
    {
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] values = line.Split(',');

                    // Assuming your CSV has columns in the order: Name, Number
                    string Name = values[0];
                    int Number = int.Parse(values[1]);

                    Contact_Data player = new Contact_Data();
                    player.Name = Name;
                    player.PhoneNumber = Number.ToString();
                    Contacts.Add(player);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading CSV file: " + e.Message);
        }
    }
    void Awake()
    {
        S_Contacts = Contacts;
        Screen.orientation = ScreenOrientation.Portrait;
        startUpPanel = GameObject.FindAnyObjectByType<StartUpPanel>();
        VideosParent.VideoPaths = startUpPanel.VideoPaths;
        VideosParent.LoadTime = startUpPanel.ThumnailLoadTime;
        ContactsaPath = startUpPanel.CSVPath;
        LoadCSV(ContactsaPath);
        ContactsParent = ContactParent;
        S_CallingPanel = callingPanel;
        int SiblingIndex = 0;
        if (ContactsParent.childCount > 0)
        {
            for (int i = 0; i < ContactsParent.childCount; i++)
            {
                DestroyImmediate(ContactsParent.GetChild(i).gameObject);
            }
        }
        for (int i = 0; i < Contacts.Count; i++)
        {
            if(ContactsParent.childCount > 0)
            {
                for (int c = 0; c < ContactsParent.childCount; c++)
                {
                    if(ContactsParent.GetChild(c).gameObject.GetComponent<Portion>() != null)
                    {
                        if (ContactsParent.GetChild(c).gameObject.GetComponent<Portion>().PortionName == Contacts[i].Name[0])
                        {
                            SiblingIndex = c + 1;
                            break;
                        }
                        else
                        {
                            if(c == ContactsParent.childCount - 1)
                            {
                                Portion portion = Instantiate(PortionPrefeb.gameObject, ContactsParent).transform.GetComponent<Portion>();
                                portion.PortionName = Contacts[i].Name[0];
                                SiblingIndex = ContactsParent.childCount;
                            }
                        }
                    }
                    else
                    {
                        if (c == ContactsParent.childCount - 1)
                        {
                            Portion portion = Instantiate(PortionPrefeb.gameObject, ContactsParent).transform.GetComponent<Portion>();
                            portion.PortionName = Contacts[i].Name[0];
                            SiblingIndex = ContactsParent.childCount;
                        }
                    }
                }
            }
            else
            {
                Portion portion = Instantiate(PortionPrefeb.gameObject, ContactsParent).transform.GetComponent<Portion>();
                portion.PortionName = Contacts[i].Name[0];
                SiblingIndex = ContactsParent.childCount;
            }

            Contact contact = Instantiate(ContactPrefeb.gameObject, ContactsParent).transform.GetComponent<Contact>();
            contact.transform.SetSiblingIndex(0);
            contact.name = Contacts[i].Name;
            contact.Name = Contacts[i].Name;
            contact.PhoneNumber = Contacts[i].PhoneNumber;
            contact.transform.SetSiblingIndex(SiblingIndex);
        }

        startUpPanel = GameObject.FindAnyObjectByType<StartUpPanel>();
        startUpPanel.SendAllData();
        if (!VideoPlayerAppDetails.WasVideoOpened)
        {
            if (App == ApptoRun.Calling)
            {
                if (Mode == Modeselect.InComing)
                {
                    StartCoroutine(SetupCall());
                }
            }
            else if (App == ApptoRun.Message)
            {
                if (Mode == Modeselect.InComing)
                {
                    StartCoroutine(SetupMessage());
                    print("MessageApp");
                }
            }
        }
        for (int i = 0; i < PrebuildMessages.Count; i++)
        {
            GetComponent<MessageApp>().GetMessage(PrebuildMessages[i].ContactName, PrebuildMessages[i].Number.ToString(), PrebuildMessages[i].Message, true, PrebuildMessages[i].Date, PrebuildMessages[i].Time, false, PrebuildMessages[i].Byme);
        }


        if (VideoPlayerAppDetails.WasVideoOpened)
        {
            VideosPanel.SetActive(true);
            VideoPlayerAppDetails.WasVideoOpened = false;
        }

        startUpPanel.DataSent = true;
    }
    IEnumerator SetupCall()
    {
        StartingAppPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartingAppPanel.SetActive(false);
        if (Mode == Modeselect.InComing)
        {
            if (ScreenMode == ScreenOffCheck.OffScreen)
            {
                BlackScreenPanel.SetActive(true);
            }
            StartCoroutine(SHowIncomingcall());
        }
    }
    IEnumerator SetupMessage()
    {
        StartingAppPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        StartingAppPanel.SetActive(false);
        if (Mode == Modeselect.InComing)
        {
            if (ScreenMode == ScreenOffCheck.OffScreen)
            {
                BlackScreenPanel.SetActive(true);
            }
            StartCoroutine(SHowIncomingMessage());
        }
    }
    public void ResetAndRecall()
    {
        if (ScreenMode == ScreenOffCheck.OffScreen)
        {
            BlackScreenPanel.SetActive(true);
        }
        CallTimer = 0;
        StartCoroutine(SHowIncomingcall());
    }

    public void BGTap()
    {
        if (IsCallStarted)
        {
            if (BlackScreenPanel.activeSelf)
            {
                BlackScreenPanel.SetActive(false);
            }
            else
            {
                BlackScreenPanel.SetActive(true);
            }
        }
    }

    IEnumerator SHowIncomingcall()
    {
        string _Name = "UnKnown";
        string PhoneNumber = "0000000000";
        if (RandomCaller)
        {
            int CallerIndex;
            CallerIndex = Random.Range(0, Contacts.Count - 1);
            _Name = Contacts[CallerIndex].Name;
            PhoneNumber = Contacts[CallerIndex].PhoneNumber;
        }
        else
        {
            _Name = Name;
            PhoneNumber = Number;
        }
        yield return new WaitForSeconds(IncomingCallWaitTime);
        if(ScreenMode == ScreenOffCheck.OffScreen)
        {
            BlackScreenPanel.SetActive(false);
        }
        CallingPanel.Name = _Name;
        CallingPanel.PhoneNumber = "Mobile" + PhoneNumber;
        StartCoroutine(ShowCall());
    }
    IEnumerator SHowIncomingMessage()
    {
        string _Name = "UnKnown";
        string PhoneNumber = "0000000000";
        if (RandomCaller)
        {
            int CallerIndex;
            CallerIndex = Random.Range(0, Contacts.Count - 1);
            _Name = Contacts[CallerIndex].Name;
            PhoneNumber = Contacts[CallerIndex].PhoneNumber;
        }
        else
        {
            _Name = Name;
            PhoneNumber = Number;
        }
        yield return new WaitForSeconds(IncomingCallWaitTime);
        if (ScreenMode == ScreenOffCheck.OffScreen)
        {
            BlackScreenPanel.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        transform.gameObject.GetComponent<MessageApp>().GetMessage(_Name, PhoneNumber, Message);
    }
    IEnumerator ShowCall()
    {
        float N = 0;
        if(ScreenMode == ScreenOffCheck.OffScreen)
        {
            N = 0.6f;
        }
        else
        {
            N = 0;
        }
        yield return new WaitForSeconds(N);
        IncomingCall.SetActive(true);
    }
    public void AnswerCall()
    {
        IsCallStarted = true;
        IncomingPanel.SetActive(true);
    }
    public void EndCall()
    {
        StartCoroutine(CallEndByTime());
    }
    IEnumerator CallEndByTime()
    {
        yield return new WaitForSeconds(1);
        IsCallStarted = false;
        IncomingCall.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(TimeText.Count > 0)
        {
            CurrunTime = TimeText[0].text;
        }
        else
        {
            CurrunTime = customTimeMin + ":" + customTimeSec;
        }

        if (ContactsPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PhonePanel.SetActive(true);
                ContactsPanel.SetActive(false);
            }
        }

        if (IsCallStarted)
        {
            CallTimer += Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            CallTimer = 0;
        }

        if(TimeType == TimeMode.CurruntTime)
        {
            foreach(TextMeshProUGUI TimeTxt in TimeText)
            {
                TimeTxt.text = System.DateTime.Now.ToString("HH") + ":" + System.DateTime.Now.ToString("mm");
            }
            MainTimeText.text = System.DateTime.Now.ToString("HH") + "\n" + System.DateTime.Now.ToString("mm");
            MainDateText.text = System.DateTime.Now.ToString("ddd, MMM dd");
        }
        else
        {
            foreach (TextMeshProUGUI TimeTxt in TimeText)
            {
                TimeTxt.text = customTimeMin.ToString() + ":" + customTimeSec.ToString();
            }
            MainTimeText.text = customTimeMin.ToString() + "\n" + customTimeSec.ToString();
        }
    }

    void UpdateTimerText()
    {
        float Min = Mathf.FloorToInt(CallTimer / 60);
        float Sec = Mathf.FloorToInt(CallTimer % 60);

        string curruntTim = string.Format("{00:00}{1:00}", Min, Sec);
        CallerTimeText.text = curruntTim[0].ToString() + curruntTim[1].ToString() + ":" + curruntTim[2].ToString() + curruntTim[3].ToString();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadGameWithDelay());
    }

    IEnumerator LoadGameWithDelay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(3);
    }
}
[System.Serializable]
public class Contact_Data
{
    public string Name;
    public string PhoneNumber;
}
public enum Modeselect
{
    OnGoing,
    InComing
}
public enum TimeMode
{
    CurruntTime,
    Custom
}
public enum ScreenOffCheck
{
    OnScreen,
    OffScreen
}
public enum ApptoRun
{
    Calling,
    Message
}