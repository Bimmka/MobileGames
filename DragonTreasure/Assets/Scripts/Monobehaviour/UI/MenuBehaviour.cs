using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    [Header("Кнопка выхода")]
    [SerializeField] private CanvasGroup exitPanelCanvasGroup;

    [Header("Canvas Group панельки с кнопками")]
    [SerializeField] private CanvasGroup buttonsPanelCanvasGroup;

    private void Awake()
    {
        SetCanvasGroupValue(exitPanelCanvasGroup, 0f, false, false);
    }

    /// <summary>
    /// Метод для выставления значений для опредленного canvasGroup
    /// </summary>
    /// <param name="canvasGroup">С каким canvasGroup работаем</param>
    /// <param name="alphaValue">Значение альфа канала</param>
    /// <param name="blockRaycastValue">Значение для параметра blockRayscast</param>
    /// <param name="interactableValue">Значение для параметра interactable</param>
    private void SetCanvasGroupValue(CanvasGroup canvasGroup, float alphaValue, bool blockRaycastValue, bool interactableValue)
    {
        canvasGroup.alpha = alphaValue;
        canvasGroup.blocksRaycasts = blockRaycastValue;
        canvasGroup.interactable = interactableValue;
    }

    /// <summary>
    /// Метод для загрузки игровой сцены
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game Scene");
    }

    /// <summary>
    /// Метод дя загрузки сцены с таблицей рекордов
    /// </summary>
    public void LoadHighScoreScene()
    {
        SceneManager.LoadScene("High Score Scene");
    }

    /// <summary>
    /// Метод для загрузки сцены "О программе"
    /// </summary>
    public void LoadAboutScene()
    {
        SceneManager.LoadScene("About Scene");
    }

    /// <summary>
    /// Метод для выхода из игры
    /// </summary>
    public void TryToExit()
    {
        SetCanvasGroupValue(exitPanelCanvasGroup, 1f, true, true);

        SetCanvasGroupValue(buttonsPanelCanvasGroup, 0.7f, false, false);
    }

    /// <summary>
    /// Метод при нажатии на кнопку "Да" , вовремя выхода из игры
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Метод при нажатии на кнопку "Нет" , вовремя выхода из игры
    /// </summary>
    public void StayInGame()
    {
        SetCanvasGroupValue(exitPanelCanvasGroup, 0f, false, false);

        SetCanvasGroupValue(buttonsPanelCanvasGroup, 1f, true, true);
    }
}
