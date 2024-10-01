using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCheck : MonoBehaviour
{
    private Camera mainCamera;
    private RectTransform rectTransform;
    public bool isInView;

    void Start()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();

        if (mainCamera == null)
        {
            Debug.LogError("Main camera not found. Make sure your camera is tagged as 'MainCamera' or assign it in the Inspector.");
        }

        if (rectTransform == null)
        {
            Debug.LogError("RectTransform not found. Attach a RectTransform component to the UI element.");
        }
    }

    void Update()
    {
        CheckIfUIElementInView();
    }

    void CheckIfUIElementInView()
    {
        if (mainCamera == null || rectTransform == null)
        {
            return;
        }

        Vector3 objectPosition = rectTransform.position;
        Vector3 viewportPoint = mainCamera.WorldToViewportPoint(objectPosition);

        isInView = IsPointInsideViewport(viewportPoint);
    }

    bool IsPointInsideViewport(Vector3 viewportPoint)
    {
        return viewportPoint.x >= 0f && viewportPoint.x <= 1f && viewportPoint.y >= 0f && viewportPoint.y <= 1f;
    }
}
