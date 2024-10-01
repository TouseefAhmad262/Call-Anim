using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhoneBottomButtons : MonoBehaviour
{
    public TextMeshProUGUI DialText;
    public TextMeshProUGUI ContactsText;

    public GameObject PhonePanel;
    public GameObject ContactsPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhonePanel.activeSelf)
        {
            DialText.fontStyle = FontStyles.Underline;
            ContactsText.fontStyle &= ~FontStyles.Underline;
            DialText.color = new Color32(50, 50, 50, 255);
            ContactsText.color = new Color32(150, 150, 150, 255);
        }else if (ContactsPanel.activeSelf)
        {
            ContactsText.fontStyle = FontStyles.Underline;
            DialText.fontStyle &= ~FontStyles.Underline;
            ContactsText.color = new Color32(50, 50, 50, 255);
            DialText.color = new Color32(150, 150, 150, 255);
        }
    }
}