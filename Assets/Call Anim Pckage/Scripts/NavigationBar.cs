using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationBar : MonoBehaviour
{
    public Button BackButton;
    public Button HomeButton;
    public static Button S_BackButton;
    public static Button S_HomeButton;

    // Start is called before the first frame update
    void Awake()
    {
        S_BackButton = BackButton;
        S_HomeButton = HomeButton;
    }
}