using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoPanelAdjustments : MonoBehaviour
{
    public List<RectTransform> rectTransforms;
    RectTransform canvas;
    void Update()
    {
        canvas = GetComponent<RectTransform>();
        for (int i = 0; i < rectTransforms.Count; i++)
        {
            rectTransforms[i].sizeDelta = new Vector2((canvas.sizeDelta.y), (canvas.sizeDelta.x));
            rectTransforms[i].anchoredPosition = new Vector2(0, 0);
        }
    }
}