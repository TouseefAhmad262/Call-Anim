using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoItem : MonoBehaviour
{
    public string VideoPath;
    public Image Thumnail;
    public TextMeshProUGUI Name;

    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        VideoPlayerAppDetails.BeReady(VideoPath);
    }
}