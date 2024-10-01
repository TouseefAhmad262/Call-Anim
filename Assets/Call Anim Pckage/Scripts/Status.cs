using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    public Image StatusImage;
    public TextMeshProUGUI StatusName;
    void Awake()
    {
        int R = Random.Range(0, Instagram.S_StatusIMages.Count);
        StatusImage.sprite = Instagram.S_StatusIMages[R];
    }
}