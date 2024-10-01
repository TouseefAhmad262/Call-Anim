using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public bool Play;
    [Space(20)]
    public float MinHight;
    public float MaxHight;

    [Space(20)]
    [Range(20,200)]
    public float Speed;

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<WaveLine>().Speed = Speed;
            transform.GetChild(i).gameObject.GetComponent<WaveLine>().Maxhight = MaxHight;
            transform.GetChild(i).gameObject.GetComponent<WaveLine>().MinHight = MinHight;
        }
    }
}