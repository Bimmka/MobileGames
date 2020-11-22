using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverUI : MonoBehaviour
{
    private CanvasGroup uiCanvasGroup;

    private void Awake()
    {
        uiCanvasGroup = GetComponent<CanvasGroup>();
        SetCanvasGroupValue(0, false, false);

        BestScoreControler.NoNewRecord += GameOver;
    }

    private void OnDisable()
    {
        BestScoreControler.NoNewRecord -= GameOver;
    }

    private void GameOver()
    {
        SetCanvasGroupValue(1, true, true);
    }

    private void SetCanvasGroupValue( float alpha, bool interactable, bool raycastBlock)
    {
        uiCanvasGroup.alpha = alpha;
        uiCanvasGroup.interactable = interactable;
        uiCanvasGroup.blocksRaycasts = raycastBlock;
    }
}
