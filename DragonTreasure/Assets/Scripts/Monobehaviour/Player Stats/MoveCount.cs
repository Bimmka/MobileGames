using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCount : MonoBehaviour
{
    [Header("Начальное количество ходов")]
    [SerializeField] private int movesNumber = 1;

    public static Action<int> DisplayMovesNumber;
    public static Action ZeroMoveCount;

    private void Awake()
    {
        ExplodedBubbleCollector.ExplodedBubbles += IncMovesNumber;

        OnTouchDown.PlayerClick += DecMovesNumber;
    }

    private void OnDisable()
    {
        ExplodedBubbleCollector.ExplodedBubbles -= IncMovesNumber;

        OnTouchDown.PlayerClick += DecMovesNumber;
    }

    private void Start()
    {
        DisplayMovesNumber?.Invoke(movesNumber);
    }

    private void IncMovesNumber(int extraMovesNumber)
    {
        movesNumber += extraMovesNumber - 1;

        DisplayMovesNumber?.Invoke(movesNumber);
    }

    private void DecMovesNumber()
    {
        movesNumber--;
        DisplayMovesNumber?.Invoke(movesNumber);
        if (movesNumber == 0) EndGame();
    }

    private void EndGame()
    {
        ZeroMoveCount?.Invoke();
    }
}
