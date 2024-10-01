using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageMainPanel : MonoBehaviour
{
    [HideInInspector]
    public bool IsOpened;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.gameObject.GetComponent<Animator>().SetBool("Close", true);
        }
    }
    void HideIt()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Close", false);
        IsOpened = false;
        gameObject.SetActive(false);
    }

    public void OpenIt()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Open", true);
    }

    void OpenDone()
    {
        transform.gameObject.GetComponent<Animator>().SetBool("Open", false);
        IsOpened = true;
    }
}
