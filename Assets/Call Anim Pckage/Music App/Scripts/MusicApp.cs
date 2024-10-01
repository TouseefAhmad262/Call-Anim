using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicApp : MonoBehaviour
{
    public List<SongData> Songs;
    SongData CurruntSong;
    int CurruntSongIndex;
    public WaveManager waveManager;
    Animator animator;
    [Space(20)]
    public GameObject PauseImage;
    public GameObject PlayImage;
    public TextMeshProUGUI SongName;
    public TextMeshProUGUI BandName;
    public Image ThumbnailImage;
    // Start is called before the first frame update
    void Start()
    {
        waveManager.Play = false;
        PlayImage.SetActive(false);
        PauseImage.SetActive(true);
        animator = GetComponent<Animator>();
        if(Songs.Count > 0 )
        {
            CurruntSong = Songs[0];
            CurruntSongIndex = 0;
            UpdateSong();
        }
    }

    void OnEnable()
    {
        NavigationBar.S_BackButton.onClick.RemoveAllListeners();
        NavigationBar.S_BackButton.onClick.AddListener(CloseApp);
        NavigationBar.S_HomeButton.onClick.RemoveAllListeners();
        NavigationBar.S_HomeButton.onClick.AddListener(CloseApp);
    }

    void OnDisable()
    {
        NavigationBar.S_BackButton.onClick.RemoveAllListeners();
        NavigationBar.S_HomeButton.onClick.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            animator.SetTrigger("Close");
        }
    }
    void CloseApp()
    {
        animator.SetTrigger("Close");
    }

    public void PlayButton()
    {
        if (waveManager.Play)
        {
            waveManager.Play = false;
            PlayImage.SetActive(false);
            PauseImage.SetActive(true);
        }
        else
        {
            waveManager.Play = true;
            PlayImage.SetActive(true);
            PauseImage.SetActive(false);
        }
    }

    void OnCloseAnim()
    {
        gameObject.SetActive(false);
    }

    void UpdateSong()
    {
        if(CurruntSong != null)
        {
            SongName.text = CurruntSong.Name;
            BandName.text = CurruntSong.BandName;
            ThumbnailImage.sprite = CurruntSong.Thumbnail;
        }
    }

    public void NextSong()
    {
        if (CurruntSongIndex < Songs.Count - 1)
        {
            CurruntSongIndex++;
            CurruntSong = Songs[CurruntSongIndex];
        }
        else
        {
            CurruntSongIndex = 0;
            CurruntSong = Songs[CurruntSongIndex];
        }
        UpdateSong();
    }

    public void PreviousSong()
    {
        if (CurruntSongIndex > 0)
        {
            CurruntSongIndex--;
            CurruntSong = Songs[CurruntSongIndex];
        }
        else
        {
            CurruntSongIndex = Songs.Count - 1;
            CurruntSong = Songs[CurruntSongIndex];
        }
        UpdateSong();
    }
}
[System.Serializable]
public class SongData
{
    public string Name;
    public string BandName;
    public Sprite Thumbnail;
}