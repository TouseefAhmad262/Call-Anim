using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CallingPanel : MonoBehaviour
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI PhoneNumberText;

    public static string Name;
    public static string PhoneNumber;

    public GameObject CallendText;
    public GameObject CallEndContros;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        PhoneNumberText.text = PhoneNumber;
        NameText.text = Name;
    }

    public void EndCall()
    {
        transform.GetComponent<Animator>().SetBool("End", true);
        StartCoroutine(HideIt());
    }

    IEnumerator HideIt()
    {
        yield return new WaitForSeconds(1.5f);

        transform.GetComponent<Animator>().SetBool("End", false);
        CallendText.SetActive(false);
        CallEndContros.SetActive(false);
        transform.gameObject.SetActive(false);
        ContactsHandler.IsCallStarted = false;
    }
}