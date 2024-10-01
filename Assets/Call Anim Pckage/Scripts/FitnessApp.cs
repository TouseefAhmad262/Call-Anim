using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessApp : MonoBehaviour
{
    public float SwipeThrushold;
    public GameObject GoalsPanels;
    public GameObject StatsPanel;
    public GameObject RouteBG;
    public GameObject RouteMapBG;

    bool IsBottomSelected;
    bool IsTopSelected;
    int BottomScreenIndex;
    int TopScreenIndex;
    // Start is called before the first frame update
    void Start()
    {
        GoalsPanels.SetActive(true);
        StatsPanel.SetActive(false);
        RouteBG.SetActive(true);
        RouteMapBG.SetActive(false);
        BottomScreenIndex = 0;
        TopScreenIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        CheckSwipe();
    }
    Vector2 touchStartPos;
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

                if (swipeValue < -SwipeThrushold)
                {
                    OnLeftSwipe();
                }
                else if (swipeValue > SwipeThrushold)
                {
                    OnRightSwipe();
                }
            }else if(touch.phase == TouchPhase.Ended)
            {
                IsBottomSelected = false;
                IsTopSelected = false;
            }
        }
    }
    void OnLeftSwipe()
    {
        if(IsBottomSelected)
        {
            if(BottomScreenIndex == 0)
            {
                GoalsPanels.GetComponent<Animator>().SetTrigger("Hide to right");
                StatsPanel.gameObject.SetActive(true);
                StatsPanel.GetComponent<Animator>().SetTrigger("Show from left");
                BottomScreenIndex = 1;
            }
        }

        if (IsTopSelected)
        {
            if(TopScreenIndex == 0)
            {
                RouteBG.GetComponent<Animator>().SetTrigger("Hide to left");
                RouteMapBG.gameObject.SetActive(true);
                RouteMapBG.GetComponent<Animator>().SetTrigger("Show From right");
                TopScreenIndex = 1;
            }
        }
        print("Left swipe");
    }
    void OnRightSwipe()
    {
        if (IsBottomSelected)
        {
            if (BottomScreenIndex == 1)
            {
                StatsPanel.GetComponent<Animator>().SetTrigger("Hide to left");
                GoalsPanels.gameObject.SetActive(true);
                GoalsPanels.GetComponent<Animator>().SetTrigger("Show From Right");
                BottomScreenIndex = 0;
            }
        }

        if (IsTopSelected)
        {
            if (TopScreenIndex == 1)
            {
                RouteMapBG.GetComponent<Animator>().SetTrigger("Hide to right");
                RouteBG.gameObject.SetActive(true);
                RouteBG.GetComponent<Animator>().SetTrigger("Show from left");
                TopScreenIndex = 0;
            }
        }
        print("Right swipe");
    }
    public void PointerTriggers(bool i)
    {
        if (!IsTopSelected)
        {
            IsBottomSelected = i;
        }
    }
    public void TopPointerTriggers(bool i)
    {
        if (!IsBottomSelected)
        {
            IsTopSelected = i;
        }
    }
}