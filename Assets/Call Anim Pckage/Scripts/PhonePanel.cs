using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonePanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            transform.gameObject.GetComponent<Animator>().SetBool("Close", true);
        }
    }
    void HideIt()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Close", false);
        gameObject.SetActive(false);
    }

    public void OpenIt()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Open", true);
    }

    void OpenDone()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Open", false);
    }
}