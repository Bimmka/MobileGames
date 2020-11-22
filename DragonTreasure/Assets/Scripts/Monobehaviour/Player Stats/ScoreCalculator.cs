using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
    private int playerScore = 0;

    public static Action<int> DisplayScore;
    public static Action<int> EndGame;
    private void Awake()
    {
        ExplodedBubbleCollector.ExplodedBubbles += CalculateScore;
        GameController.GameEnd += GameOver;
    }

    private void OnDisable()
    {
        ExplodedBubbleCollector.ExplodedBubbles -= CalculateScore;
        GameController.GameEnd -= GameOver;
    }

    private void Start()
    {
        DisplayScore?.Invoke(playerScore);
    }

    private void CalculateScore(int explodedBubbles)
    {
        playerScore += explodedBubbles;

        DisplayScore?.Invoke(playerScore);
    }

    private void GameOver()
    {
        EndGame?.Invoke(playerScore);
    }
}
