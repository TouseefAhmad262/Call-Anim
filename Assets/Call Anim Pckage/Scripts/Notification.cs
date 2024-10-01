using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Message;
    public Animator NotificationAnimator;
    
    public float OnScreenTime;
    // Start is called before the first frame update
    void Awake()
    {
       
    }

    void Start()
    {
        
    }
    void OnEnable()
    {
        StartCoroutine(HideNotification());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator HideNotification()
    {
        yield return new WaitForSeconds(OnScreenTime);
        NotificationAnimator.SetBool("OnScreen", true);
    }

    public void HideNotificationEarlier()
    {
        NotificationAnimator.SetBool("OnScreen", true);
    }

    void HidetheNotification()
    {
        NotificationAnimator.gameObject.SetActive(false);
    }
}