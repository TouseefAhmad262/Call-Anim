using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessAppControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<FitnessAppAnimationsController>().Splash.SetActive(true);
            GetComponent<Animator>().enabled = true;
            GetComponent<Animator>().SetTrigger("Close");
        }
    }
}