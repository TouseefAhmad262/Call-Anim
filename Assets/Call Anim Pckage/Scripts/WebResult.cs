using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class WebResult : MonoBehaviour
{
    public string Title;
    public string Details;
    [Space(20)]
    public TextMeshProUGUI Text;

    void Awake()
    {
        UpdateValues();
    }
    public void UpdateValues()
    {
        Text.text = "<b><color=#4D78D3>" + Title + "</color></b>" + "\n\n\n" + Details;
    }
}