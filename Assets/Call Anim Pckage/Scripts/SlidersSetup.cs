using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlidersSetup : MonoBehaviour
{
    public Slider Slider;
    public TextMeshProUGUI PercentageText;
    void Awake()
    {
        Slider.onValueChanged.AddListener(delegate { OnsliderValueChanges(); });
    }

    void OnsliderValueChanges()
    {
        PercentageText.text = GetRatio(Slider.value, Slider.minValue, Slider.maxValue, 0, 100).ToString("f0") + "%";
    }

    public float GetRatio(float ValueToRelate, float RelatedMinvalue, float RelatedMaxValue, float MinTargetValue, float MaxTargetVAlue)
    {
        float ratioA = (ValueToRelate - RelatedMinvalue) / (RelatedMaxValue - RelatedMinvalue);
        float newB = MinTargetValue + (MaxTargetVAlue - MinTargetValue) * ratioA;
        return newB;
    }
}