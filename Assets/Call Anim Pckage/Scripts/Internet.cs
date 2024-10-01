using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Internet : MonoBehaviour
{
    public List<ResultData> webResults;
    public Transform ResultsParent;
    public WebResult ResultPrefeb;

    [Space(20)]
    public OtherRefrences otherRefrences;
    void OnEnable()
    {
        NavigationBar.S_BackButton.onClick.RemoveAllListeners();
        NavigationBar.S_BackButton.onClick.AddListener(StartClosingInternet);
    }
    void Awake()
    {
        for (int i = 0; i < webResults.Count; i++)
        {
            GenrateResult(webResults[i]);
        }
    }

    void Update()
    {
        if (otherRefrences.MainPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                StartClosingInternet();
            }
        }
        else if(otherRefrences.ResultsPage.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                otherRefrences.MainPanel.SetActive(true);
                otherRefrences.ResultsPage.SetActive(false);
            }
        }
    }

    void GenrateResult(ResultData Data)
    {
        WebResult Res = Instantiate(ResultPrefeb.gameObject, ResultsParent).GetComponent<WebResult>();
        Res.Title = Data.Title;
        Res.Details = Data.Details;
        Res.gameObject.GetComponent<Button>().onClick.AddListener(() => print(Res.Title + "\nLink Pressed"));
        Res.UpdateValues();
    }

    void RemoveAllResults()
    {
        for (int i = 0; i < ResultsParent.transform.childCount; i++)
        {
            Destroy(ResultsParent.transform.GetChild(0).gameObject);
        }
    }

    void CloseInternet()
    {
        gameObject.SetActive(false);
    }

    void OnOpenedInternet()
    {
        StartCoroutine(DisableAnimator());
    }

    IEnumerator DisableAnimator()
    {
        yield return new WaitForSeconds(1);
      //  GetComponent<Animator>().enabled = false;
    }

    void StartClosingInternet()
    {
        GetComponent<Animator>().enabled = true;
        GetComponent<Animator>().Play("Internet Close");
    }

    public void ShowLinks()
    {
        otherRefrences.LoadingBar.gameObject.SetActive(true);
        otherRefrences.LoadingBar.value = 0;
        StartCoroutine(OpenLniksWindow());
    }

    IEnumerator OpenLniksWindow()
    {
        float Decrese = 1;
        while(otherRefrences.LoadingBar.value != 1)
        {
            otherRefrences.LoadingBar.value += 0.01f * Decrese;
            if(Decrese > 0.3f)
            {
                Decrese -= 0.01f;
            }
            yield return null;
        }

        otherRefrences.LoadingBar.gameObject.SetActive(false);
        otherRefrences.MainPanel.SetActive(false);
        otherRefrences.ResultsPage.SetActive(true);
    }
}
[System.Serializable]
public class ResultData
{
    public string Title;

    [TextArea(3, 15)]
    public string Details;
}
[System.Serializable]
public class OtherRefrences
{
    public GameObject MainPanel;
    public GameObject ResultsPage;
    [Space(20)]
    public Slider LoadingBar;
}