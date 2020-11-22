using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static Action GameEnd;

    private void Awake()
    {
        MoveCount.ZeroMoveCount += EndGame;
    }

    private void OnDisable()
    {
        MoveCount.ZeroMoveCount -= EndGame;
    }

    private void EndGame()
    {
        GameEnd?.Invoke();
    }
}
