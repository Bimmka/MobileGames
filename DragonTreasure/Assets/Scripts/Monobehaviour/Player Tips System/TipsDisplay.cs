using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsDisplay : MonoBehaviour
{
    [Header("CanvasGroup для отображения подсказок")]
    [SerializeField] private CanvasGroup canvasTipCanvasGroup;

    [Header("Объект с видео")]
    [SerializeField] private GameObject videoObject;

    public static Action DisplayFirstMove;
    private void Awake()
    {
        SetCanvasGroupValue(1, true, true);
    }

    public void CloseTip()
    {
        SetCanvasGroupValue(0, false, false);
        videoObject.SetActive(false);

        DisplayFirstMove?.Invoke();
    }

    private void SetCanvasGroupValue(float alpha, bool interactable, bool raycastBlock)
    {
        canvasTipCanvasGroup.alpha = alpha;
        canvasTipCanvasGroup.interactable = interactable;
        canvasTipCanvasGroup.blocksRaycasts = raycastBlock;
    }
}
