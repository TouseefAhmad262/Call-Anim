using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopBarHandler : MonoBehaviour
{
    public Image WIFI;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckWifi());
    }

    IEnumerator CheckWifi()
    {
        while (FindAnyObjectByType<StartUpPanel>() == null)
        {
            yield return null;
        }

        if (StartUpPanel.WIFI)
        {
            WIFI.gameObject.SetActive(true);
        }
        else
        {
            WIFI.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
