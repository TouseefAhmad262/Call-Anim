using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Post : MonoBehaviour
{
    public Image PostImage;
    public Image Logo;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI SideName;
    void Awake()
    {
        int R = Random.Range(0, Instagram.S_StatusIMages.Count);
        PostImage.sprite = Instagram.S_StatusIMages[R];
        
        int P = Random.Range(0, Instagram.S_StatusIMages.Count);
        Logo.sprite = Instagram.S_StatusIMages[P];
    }
}