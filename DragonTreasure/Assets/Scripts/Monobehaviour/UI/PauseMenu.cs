using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("CanvasGroup Pause Menu")]
    [SerializeField] private CanvasGroup pauseMenuCanvasGroup;

    public static Action<bool> GameStop;

    private void Awake()
    {
        SetCanvasGroupValue(0, false, false);
    }

    
    private void SetCanvasGroupValue(float alpha, bool interactable, bool raycastBlock)
    {
        pauseMenuCanvasGroup.alpha = alpha;
        pauseMenuCanvasGroup.interactable = interactable;
        pauseMenuCanvasGroup.blocksRaycasts = raycastBlock;
    }

    public void TryGoToMenu()
    {
        Time.timeScale = 0f;

        SetCanvasGroupValue(1, true, true);

        GameStop?.Invoke(true);
    }

    public void StayInGame()
    {
        Time.timeScale = 1f;

        SetCanvasGroupValue(0, false, false);

        GameStop?.Invoke(false);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main Menu Scene");
    }
}
