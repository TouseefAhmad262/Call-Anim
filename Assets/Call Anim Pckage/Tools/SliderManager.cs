using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    public TextMeshProUGUI PercentageTxt;
    void Awake()
    {
        onvalueChange();
    }
    public void onvalueChange()
    {
        PercentageTxt.text = gameObject.GetComponent<Slider>().value.ToString("f2") + "s";
    }
}