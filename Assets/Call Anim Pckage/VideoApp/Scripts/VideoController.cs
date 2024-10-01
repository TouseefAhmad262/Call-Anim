using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoPlayer VideoPlayer;
    public Slider TimeLine;
    public Button PlayButton;
    public CanvasGroup canvasGroup;
    public float StadByTime;
    public float FadeSpeed;
    bool IsTimelineSelected;
    public int SwipeThrushold;
    bool ShowingNavigation;
    public GameObject Navigationbar;
    bool ShowingControls;

    [Space(20)]
    public Sprite PlaySprite;
    public Sprite PauseSprite;
    public GameObject Sign2x;
    public CanvasGroup VolumeCanvasGroup;
    public Slider VolumeSlider;
    void Awake()
    {
        VideoPlayerAppDetails.WasVideoOpened = true;
        VideoPlayer.source = VideoSource.Url;
        VideoPlayer.url = VideoPlayerAppDetails.URL;
        PlayButton.image.sprite = PauseSprite;
    }

    void Start()
    {
        VideoPlayer.Pause();
        if (VideoPlayer.isPaused)
        {
            print("Paused");
        }
        VideoPlayer.prepareCompleted += ad => OnPrepared();
        VideoPlayer.Prepare();
        StartCoroutine(HideControlsByTime());
    }

    void OnPrepared()
    {
        TimeLine.maxValue = (float)VideoPlayer.length;
        print("Prepared");
        VideoPlayer.Play();
    }

    void Update()
    {
        if (!IsTimelineSelected)
        {
            TimeLine.value = (float)VideoPlayer.time;
        }
        else
        {
            VideoPlayer.time = TimeLine.value;
        }
        CheckSwipe();
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
        if (IsVolumeShowing)
        {
            VolumeSlider.value = VideoPlayer.GetDirectAudioVolume(0);
        }
    }
    IEnumerator Hidenavigation()
    {
        yield return new WaitForSeconds(5);
        Navigationbar.gameObject.GetComponent<Animator>().Play("Nav Down", 0);
        ShowingNavigation = false;
    }
    IEnumerator FadeControls(float Speed, CanvasGroup Group, bool FadeIn)
    {
        float FinalValue = 0;
        while (FinalValue <= 1)
        {
            FinalValue += Speed * Time.deltaTime;
            if (FadeIn)
            {
                Group.alpha = Mathf.Lerp(0, 1, FinalValue);
            }
            else
            {
                Group.alpha = Mathf.Lerp(1, 0, FinalValue);
            }
            yield return null;
        }
        if (!FadeIn)
        {
            Group.interactable = false;
            StopCoroutine(HideControlsByTime());
        }
        else
        {
            Group.interactable = true;
            StartCoroutine(HideControlsByTime());
        }
    }
    public void Show_HideControls()
    {
        if (canvasGroup.interactable)
        {
            StartCoroutine(FadeControls(FadeSpeed, canvasGroup, false));
        }
        else
        {
            StartCoroutine(FadeControls(FadeSpeed, canvasGroup, true));
        }
    }

    IEnumerator HideControlsByTime()
    {
        yield return new WaitForSeconds(5);
        StartCoroutine(FadeControls(FadeSpeed, canvasGroup, false));
    }
    public void SliderSelected()
    {
        IsTimelineSelected = true;
    }
    public void SliderUnselected()
    {
        IsTimelineSelected = false;
    }

    public void Pause_PLay()
    {
        if (VideoPlayer.isPlaying)
        {
            VideoPlayer.Pause();
            PlayButton.image.sprite = PlaySprite;
        }
        else
        {
            VideoPlayer.Play();
            PlayButton.image.sprite = PauseSprite;
        }
    }
    public Vector2 touchStartPos;
    bool IsVolumeShowing;
    void CheckSwipe()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                float swipeValue = touch.position.x - touchStartPos.x;
                float SwipeUpValue = touch.position.y - touchStartPos.y;

                if (swipeValue > SwipeThrushold)
                {
                    // Left Swipe
                }
                else if (SwipeUpValue < -SwipeThrushold)
                {
                    if(touchStartPos.y > 200)
                    {
                        if(SwipeUpValue < -(SwipeThrushold * 15))
                        {
                            VideoPlayer.playbackSpeed = 2;
                            Sign2x.SetActive(true);
                        }
                    }
                }

                if(swipeValue > SwipeThrushold)
                {
                    if(touchStartPos.x < 200)
                    {
                        if (!ShowingNavigation)
                        {
                            Navigationbar.gameObject.GetComponent<Animator>().Play("Nav Up", 0);
                            ShowingNavigation = true;
                            StartCoroutine(Hidenavigation());
                        }
                    }
                    else
                    {
                        if(VideoPlayer.GetDirectAudioVolume(0) < 1)
                        {
                            if (!IsVolumeShowing)
                            {
                                StartCoroutine(ShowVolume());
                            }
                            VideoPlayer.SetDirectAudioVolume(0, VideoPlayer.GetDirectAudioVolume(0) + (swipeValue * 0.000008f));
                        }
                    }
                }else if (swipeValue < -SwipeThrushold)
                {
                    if (touchStartPos.x > 200)
                    {
                        if (VideoPlayer.GetDirectAudioVolume(0) > 0)
                        {
                            if (!IsVolumeShowing)
                            {
                                StartCoroutine(ShowVolume());
                            }
                            VideoPlayer.SetDirectAudioVolume(0, VideoPlayer.GetDirectAudioVolume(0) + (swipeValue * 0.000008f));
                        }
                    }
                }

            }else if(touch.phase == TouchPhase.Ended)
            {
                VideoPlayer.playbackSpeed = 1;
                Sign2x.SetActive(false);
                if(VideoPlayer.GetDirectAudioVolume(0) > 1)
                {
                    VideoPlayer.SetDirectAudioVolume(0, 1);
                }
                else if (VideoPlayer.GetDirectAudioVolume(0) < 0)
                {
                    VideoPlayer.SetDirectAudioVolume(0, 0);
                }
                StartCoroutine(HideVolume());
            }
        }
    }

    IEnumerator ShowVolume()
    {
        IsVolumeShowing = true;
        while (VolumeCanvasGroup.alpha < 1)
        {
            VolumeCanvasGroup.alpha += 0.009f;
            yield return null;
        }

        VolumeCanvasGroup.alpha = 1;
    }

    IEnumerator HideVolume()
    {
        yield return new WaitForSeconds(2);
        IsVolumeShowing = false;
        while (VolumeCanvasGroup.alpha > 0)
        {
            VolumeCanvasGroup.alpha -= 0.009f;
            yield return null;
        }

        VolumeCanvasGroup.alpha = 0;
    }

    public void GoBack()
    {
        SceneManager.LoadScene(1);
    }
}