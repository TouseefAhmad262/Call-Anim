using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public bool Move;
    float Posy;
    RectTransform rectTransform;
    float ToExpand;
    bool CanExpand;
    float Timer;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Posy = rectTransform.anchoredPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Move)
        {
            if (CanExpand)
            {
                Show();
                if (Timer < GameManager.S_StayTime)
                {
                    Timer += Time.deltaTime;
                }
                else
                {
                    Timer = 0;
                    CanExpand = false;
                }
            }
            else
            {
                Hide();
                if (Timer < GameManager.S_DelayTime)
                {
                    Timer += Time.deltaTime;
                }
                else
                {
                    Timer = 0;
                    CanExpand = true;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Player")
        {
            if (!GameManager.IsGameOver)
            {
                GameManager.PlayerHit();
            }
        }
    }
    void Show()
    {
        if (rectTransform.anchoredPosition.y < Posy + GameManager.S_BladeOfset)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y + Time.deltaTime * GameManager.S_BladeSpeed);
        }
    }

    void Hide()
    {
        if (rectTransform.anchoredPosition.y > Posy)
        {
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - Time.deltaTime * GameManager.S_BladeSpeed);
        }
    }
}