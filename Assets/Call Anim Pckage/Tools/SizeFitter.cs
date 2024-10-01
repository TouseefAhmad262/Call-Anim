using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeFitter : MonoBehaviour
{
    public RectTransform ObjectToRelate;
    RectTransform OnneedToRelate;
    public Vector2 Paddings;
    public MatchType Relatetype;
    void Start()
    {
        OnneedToRelate = GetComponent<RectTransform>();
    }
    void Update()
    {
        switch (Relatetype)
        {
            case MatchType.WidthAndHeight:
                OnneedToRelate.sizeDelta = new Vector2(ObjectToRelate.sizeDelta.x + Paddings.x, ObjectToRelate.sizeDelta.y + Paddings.y);
                break;
            case MatchType.OnlyHeight:
                OnneedToRelate.sizeDelta = new Vector2(OnneedToRelate.sizeDelta.x, ObjectToRelate.sizeDelta.y + Paddings.y);
                break;
            case MatchType.OnlyWidth:
                OnneedToRelate.sizeDelta = new Vector2(ObjectToRelate.sizeDelta.x + Paddings.x, OnneedToRelate.sizeDelta.y);
                break;
        }
    }

    public enum MatchType
    {
        WidthAndHeight,
        OnlyWidth,
        OnlyHeight
    }
}