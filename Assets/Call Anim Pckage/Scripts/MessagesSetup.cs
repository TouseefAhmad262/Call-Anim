using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics.Contracts;
using System.IO;

public class MessagesSetup : MonoBehaviour
{
    public static string CsvPath;
    public List<string> Names;
    public static List<string> S_Names;
    public List<int> Numbers;
    public List<MessagesDetails> Messages;
    public Transform MessagesParent;
    public GameObject MessageDataObject;
    // Start is called before the first frame update
    void Awake()
    {
        CsvPath = GetComponent<StartUpPanel>().CSVPath;
        LoadCSV(CsvPath);
        S_Names = Names;
    }

    // Update is called once per frame
    string TempPath;
    void Update()
    {
        TempPath = CsvPath;
        CsvPath = GetComponent<StartUpPanel>().CSVPath;
    }
    void LateUpdate()
    {
        if (CsvPath != TempPath)
        {
            LoadCSV(CsvPath);
            print("Path Has been changed, New data has been imported Succesfully");
        }
    }

    void LoadCSV(string filePath)
    {
        Names.Clear();
        Numbers.Clear();
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

                    Names.Add(Name);
                    Numbers.Add(Number);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error reading CSV file: " + e.Message);
        }
    }
    public void AddMessage()
    {
        MessageDataObject dataObj = Instantiate(MessageDataObject, MessagesParent).gameObject.GetComponent<MessageDataObject>();
        dataObj.transform.SetSiblingIndex(dataObj.transform.parent.childCount - 3);
        MessagesDetails details = new MessagesDetails();
        details.Contact = dataObj.Contact;
        details.Message = dataObj.Message;
        details.Byme = dataObj.Byme;
        details.Time = dataObj.Time;
        details.Date = dataObj.Date;
        Messages.Add(details);
        if(Messages.Count > 1)
        {
            Messages[Messages.Count - 1].Contact.value = Messages[Messages.Count - 2].Contact.value;
            Messages[Messages.Count - 1].Time.text = Messages[Messages.Count - 2].Time.text;
            Messages[Messages.Count - 1].Date.text = Messages[Messages.Count - 2].Date.text;
        }
    }
}
[System.Serializable]
public class MessagesDetails
{
    public TMP_Dropdown Contact;
    public TMP_InputField Message;
    public TMP_InputField Time;
    public TMP_InputField Date;
    public Toggle Byme;
}