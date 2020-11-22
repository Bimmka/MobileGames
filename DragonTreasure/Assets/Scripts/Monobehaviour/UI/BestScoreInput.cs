using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BestScoreInput : MonoBehaviour
{
    [Header("CanvasGroup")]
    [SerializeField] private CanvasGroup gameObjectCanvasGroup;

    [Header("InputField куда вводят имя игрока")]
    [SerializeField] private InputField inputFieldForPlayerName;

    [Header("Кнопка OK после ввода")]
    [SerializeField] private CanvasGroup okButtonCanvasGroup;

    public static Action<string> PlayerName;
    private void Awake()
    {
        BestScoreControler.NewRecordPlayerName += DisplayInputField;

        SetCanvasGroupValue(gameObjectCanvasGroup, 0, false, false);
        SetCanvasGroupValue(okButtonCanvasGroup, 0, false, false);
    }

    private void OnDisable()
    {
        BestScoreControler.NewRecordPlayerName -= DisplayInputField;
    }


    private void DisplayInputField()
    {
        SetCanvasGroupValue(gameObjectCanvasGroup, 1, true, true);
    }

    private void SetCanvasGroupValue(CanvasGroup canvasGroupName, float alpha, bool interactable, bool raycastBlock)
    {
        canvasGroupName.alpha = alpha;
        canvasGroupName.interactable = interactable;
        canvasGroupName.blocksRaycasts = raycastBlock;
    }

    public void GetPlayerName()
    {
        PlayerName?.Invoke(inputFieldForPlayerName.text);

        SetCanvasGroupValue(gameObjectCanvasGroup, 0, false, false);
        SetCanvasGroupValue(okButtonCanvasGroup, 1, true, true);
    }
}
