using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Details : MonoBehaviour
{
    public TextMeshProUGUI MobileNumberTxt;
    public string Name;
    public Button CallButton;
    // Start is called before the first frame update
    void Start()
    {
        CallButton.onClick.AddListener(ShowCalling);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowCalling()
    {
        CallingPanel.Name = Name;
        CallingPanel.PhoneNumber = MobileNumberTxt.text;
        ContactsHandler.S_CallingPanel.SetActive(true);
    }
}