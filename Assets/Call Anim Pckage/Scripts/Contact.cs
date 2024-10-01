using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Contact : MonoBehaviour
{
    public string Name;
    public string PhoneNumber;


    [Space(20)]
    public Image Avatar;
    public TextMeshProUGUI NameTxt;
    public TextMeshProUGUI FirstLaterText;
    public Details CallOptionsPrefeb;
    // Start is called before the first frame update
    void Start()
    {
        Avatar.color = new Color(Random.Range(0.4f, 0.85f), Random.Range(0.4f, 0.85f), Random.Range(0.4f, 0.85f), 255);
        FirstLaterText.text = Name[0].ToString();
        NameTxt.text = Name;
        gameObject.GetComponent<Button>().onClick.AddListener(OnContactClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnContactClick()
    {
        HideAllDetails();
        Details details = Instantiate(CallOptionsPrefeb.gameObject, ContactsHandler.ContactsParent).gameObject.GetComponent<Details>();
        details.MobileNumberTxt.text = "Mobile " + PhoneNumber;
        details.Name = Name;
        details.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
    }

    void HideAllDetails()
    {
        for (int i = 0; i < ContactsHandler.ContactsParent.childCount; i++)
        {
            if (ContactsHandler.ContactsParent.GetChild(i).gameObject.GetComponent<Details>() != null)
            {
                Destroy(ContactsHandler.ContactsParent.GetChild(i).gameObject);
            }
        }
    }
}