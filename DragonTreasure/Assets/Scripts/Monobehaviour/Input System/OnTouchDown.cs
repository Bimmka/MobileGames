using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnTouchDown : MonoBehaviour
{
    [Header("Иконка для курсора")]
    [SerializeField] private Texture2D cursorTexture;

    private Camera mainCamera;

    private bool isGameStopped = false;
    private bool isFirstMove = true;

    private Vector3 firstCellPosition;

    public static Action<Cell> PlayerClickedOnCell;
    public static Action PlayerClick;

    private void Awake()
    {
        mainCamera = Camera.main;

        GameController.GameEnd += GameOver;

        PauseMenu.GameStop += StopPlayerClick;

        FirstMoveCalculator.FirstMoveCell += SetFirstCellPosition;

        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        GameController.GameEnd -= GameOver;

        FirstMoveCalculator.FirstMoveCell -= SetFirstCellPosition;

        PauseMenu.GameStop -= StopPlayerClick;
    }
    private void Update()
    {
        if (!isGameStopped)
        {
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touches.Length > 0)
        {
            if (Input.GetTouch(0).phase.Equals(TouchPhase.Began)) HandlinClickTouch();
        }
#elif UNITY_STANDALONE || UNITY_WEBGL
            if (Input.GetMouseButtonDown(0)) HandlingClickMouse();
#endif
        }

    }
    private void HandlingClickMouse()
    {
        SetRaycast(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }

    private void HandlinClickTouch()
    {
        SetRaycast(mainCamera.ScreenToWorldPoint(Input.touches[0].position));
    }

    private void SetRaycast(Vector2 origin)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero);
        if (hit.collider != null)
        {
            Cell cellComponent = hit.collider.GetComponent<Cell>();
            if (cellComponent != null && cellComponent.CurrentBubble != null)
            {
                if (isFirstMove) FirstMoveClick(cellComponent);
                else Click(cellComponent);                
            }
        }      
    }

    private void Click(Cell cellComponent)
    {
        PlayerClickedOnCell(cellComponent);
        PlayerClick();
    }

    private void SetFirstCellPosition(Vector3 position)
    {
        firstCellPosition = position;
    }
    
    private void FirstMoveClick(Cell cell)
    {
        if (firstCellPosition == cell.transform.position)
        {
            isFirstMove = false;
            Click(cell);
        }
    }

    private void StopPlayerClick(bool value)
    {
        isGameStopped = value;
    }
    private void GameOver()
    {
        isGameStopped = true;
    }
}
