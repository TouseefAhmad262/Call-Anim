using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PhoneManager : MonoBehaviour
{
    public Animator NavigationAnimator;
    public float SwipeThrushold;

    void Start()
    {
        NavigationAnimator.SetBool("State", false);
    }

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
                float swipeValue = touch.position.y - touchStartPos.y;

                if (swipeValue < -SwipeThrushold)
                {
                    
                }
                else if (swipeValue > SwipeThrushold)
                {
                    OnSwipeUp();
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                
            }
        }
    }

    void OnSwipeUp()
    {
        NavigationAnimator.SetBool("State", true);
        StopAllCoroutines();
        StartCoroutine(CloseNavigation());
    }

    IEnumerator CloseNavigation()
    {
        yield return new WaitForSeconds(5);
        NavigationAnimator.SetBool("State", false);
    }
}