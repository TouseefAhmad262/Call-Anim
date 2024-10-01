using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instagram : MonoBehaviour
{
    public static List<Sprite> S_StatusIMages;
    public List<Sprite> StatusImages;

    public Transform StatusParent;
    public Transform PostParent;
    public Status StatusPrefeb;
    public Post PostPrefeb;

    void Awake()
    {
        S_StatusIMages = StatusImages;
        for (int i = 0; i < 5; i++)
        {
            GenrateStatus();
        }
        for (int i = 0; i < 15; i++)
        {
            GenratePosts();
        }
    }

    void OnEnable()
    {
        NavigationBar.S_BackButton.onClick.RemoveAllListeners();
        NavigationBar.S_BackButton.onClick.AddListener(StartClosingInstagram);
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            StartClosingInstagram();
        }
    }

    void GenrateStatus()
    {
        int R = Random.Range(0, ContactsHandler.S_Contacts.Count);
        Status status = Instantiate(StatusPrefeb.gameObject, StatusParent).GetComponent<Status>();
        status.StatusName.text = ContactsHandler.S_Contacts[R].Name;
    }
    void GenratePosts()
    {
        int R = Random.Range(0, ContactsHandler.S_Contacts.Count);
        Post post = Instantiate(PostPrefeb.gameObject, PostParent).GetComponent<Post>();
        post.Name.text = ContactsHandler.S_Contacts[R].Name;
        post.SideName.text = ContactsHandler.S_Contacts[R].Name + " - Official";
    }

    void CloseInstagram()
    {
        gameObject.SetActive(false);
    }

    void OnOpenedInstagram()
    {
        StartCoroutine(DisableAnimator());
    }

    void StartClosingInstagram()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Instagram Closing");
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(1);
       // GetComponent<Animator>().enabled = false;
    }
}