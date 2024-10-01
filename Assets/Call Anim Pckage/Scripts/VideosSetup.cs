using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class VideosSetup : MonoBehaviour
{
    public GameObject LinkPrefeb;
    public Transform Parent;
    
    public void AddVideo(string URL)
    {
        TextMeshProUGUI URLText = Instantiate(LinkPrefeb, Parent).gameObject.GetComponent<TextMeshProUGUI>();
        URLText.text = URL;
        URLText.gameObject.transform.SetSiblingIndex(Parent.transform.childCount - 4);
        gameObject.GetComponent<StartUpPanel>().VideoPaths.Add(URL);
    }
}