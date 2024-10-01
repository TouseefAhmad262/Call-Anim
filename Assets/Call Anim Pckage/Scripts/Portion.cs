using TMPro;
using UnityEngine;

public class Portion : MonoBehaviour
{
    public char PortionName;
    [Space(20)]
    public TextMeshProUGUI PortionText;
    void Start()
    {
        PortionText.text = PortionName.ToString();
    }
}