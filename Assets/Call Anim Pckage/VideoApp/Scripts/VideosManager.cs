using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideosManager : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseApp();
        }
    }

    public void OpenApp()
    {
        gameObject.SetActive(true);
        animator.SetTrigger("Open");
    }
    public void CloseApp()
    {
        animator.SetTrigger("Close");
    }

    void OnCloseAnim()
    {
        gameObject.SetActive(false);
    }
}