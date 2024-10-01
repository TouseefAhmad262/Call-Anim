using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoPlayerAppDetails : MonoBehaviour
{
    public static string URL;
    public static bool WasVideoOpened;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public static void BeReady(string url)
    {
        URL = url;
        SceneManager.LoadScene(2);
    }
}