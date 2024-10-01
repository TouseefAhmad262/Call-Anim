using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveLine : MonoBehaviour
{
    WaveManager waveManager;
    RectTransform rectTransform;
    float TargetHight;
    bool Animating_Up;
    bool Animating_Down;
    [HideInInspector]public float Speed, Maxhight, MinHight;
    // Update is called once per frame

    void Awake()
    {
        waveManager = GetComponentInParent<WaveManager>();
        rectTransform = GetComponent<RectTransform>();
        TargetHight = Random.Range(MinHight, Maxhight);
        if(TargetHight > rectTransform.sizeDelta.y)
        {
            Animating_Up = true;
            Animating_Down = true;
        }
        else
        {
            Animating_Up = false;
            Animating_Down = true;
        }

    }
    void Update()
    {
        if (waveManager.Play)
        {
            if(TargetHight > rectTransform.sizeDelta.y)
            {
                if(!Animating_Down)
                {
                    TargetHight = Random.Range(MinHight, Maxhight);
                    Animating_Down = false;
                    return;
                }
                else
                {
                    if (rectTransform.sizeDelta.y < TargetHight)
                    {
                        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y + (Speed * Time.deltaTime));

                    }
                    else
                    {
                        TargetHight = Random.Range(MinHight, Maxhight);
                    }
                }
                Animating_Up = true;
            }
            else
            {
                if (Animating_Up)
                {
                    TargetHight = Random.Range(MinHight, Maxhight);
                    Animating_Up = false;
                    return;
                }
                else
                {
                    Animating_Down = true;
                    if (rectTransform.sizeDelta.y > TargetHight)
                    {
                        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - (Speed * Time.deltaTime));
                    }
                    else
                    {
                        TargetHight = Random.Range(MinHight, Maxhight);
                    }
                }
            }
        }
        else
        {
            if (rectTransform.sizeDelta.y > MinHight)
            {
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - (Speed * Time.deltaTime));
            }
        }
    }

}