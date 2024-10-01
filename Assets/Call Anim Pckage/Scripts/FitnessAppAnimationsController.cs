using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessAppAnimationsController : MonoBehaviour
{
    public GameObject Splash;
    void Hide()
    {
        gameObject.SetActive(false);
    }
    void Show()
    {
        gameObject.SetActive(true);
    }

    void OnSplashEnds()
    {
        Splash.SetActive(false);
        GetComponent<Animator>().enabled = false;
    }
}