using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageItem : MonoBehaviour {

    public float topOffset = 5;
    public float bottomOffset = 5;

    private RectTransform rectTransform;
    private RectTransform headRect;
    private RectTransform textRect;

    public float Height
    {
        get
        {
            return rectTransform.sizeDelta.y;
        }
    }

    // Use this for initialization
    void Awake () {
        rectTransform = GetComponent<RectTransform>();
        headRect = rectTransform.Find("HeadPortrait").GetComponent<RectTransform>();
        textRect = rectTransform.Find("TextBg/Text").GetComponent<RectTransform>();

        Invoke("WaitCalc", 0.05f);
    }

    void WaitCalc()
    {
        float tmpHeight = Mathf.Max(headRect.sizeDelta.y, textRect.sizeDelta.y + topOffset + bottomOffset);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, topOffset + bottomOffset + tmpHeight);
    }
}
