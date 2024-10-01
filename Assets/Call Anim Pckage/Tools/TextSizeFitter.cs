using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextSizeFitter : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Vector2 MaxLimits;
    public Vector2 MinLimits;
    // Start is called before the first frame update
    void Start()
    {
        if(Text == null)
        {
            if(transform.GetComponent<TextMeshProUGUI>() != null)
            {
                Text = transform.GetComponent<TextMeshProUGUI>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(MaxLimits.x != 0 && MinLimits.x != 0)
        {
             if (!isXLimitReached() && !isXMinLimitReached())
             {
                Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.preferredWidth, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
             }
             else
             {
                if (isXLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxLimits.x, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
                else if(isXMinLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(MinLimits.x, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
             }
        }
        else
        {
            if(MaxLimits.x != 0)
            {
                if (!isXLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.preferredWidth, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
                else
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(MaxLimits.x, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
            else if(MinLimits.x != 0)
            {
                if (!isXMinLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.preferredWidth, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
                else
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(MinLimits.x, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
                }
            }
            else
            {
                Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.preferredWidth, Text.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            }
        }

        if(MaxLimits.y != 0 && MinLimits.y != 0)
        {
            if (!isYLimitReached() && !isYMinLimitReached())
            {
                Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, Text.preferredHeight);
            }
            else
            {
                if (isYLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, MaxLimits.y);
                }
                else if(isYMinLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, MinLimits.y);
                }
            }
        }
        else
        {
            if(MaxLimits.y != 0)
            {
                if (!isYLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, Text.preferredHeight);
                }
                else
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, MaxLimits.y);
                }
            }else if(MinLimits.y != 0)
            {
                if (!isYMinLimitReached())
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, Text.preferredHeight);
                }
                else
                {
                    Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, MinLimits.y);
                }
            }
            else
            {
                Text.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Text.gameObject.GetComponent<RectTransform>().sizeDelta.x, Text.preferredHeight);
            }
        }
    }

    bool isYLimitReached()
    {
        if(Text.preferredHeight < MaxLimits.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    bool isYMinLimitReached()
    {
        if (Text.preferredHeight > MinLimits.y)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool isXLimitReached()
    {
        if (Text.preferredWidth < MaxLimits.x)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    bool isXMinLimitReached()
    {
        if (Text.preferredWidth > MinLimits.x)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}