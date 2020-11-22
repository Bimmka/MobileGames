using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
    [Header("Куда отображать количество ходов у игрока")]
    [SerializeField] private TMP_Text playerMovesNumberText;

    [Header("Куда отображать количество набранных очков игрока")]
    [SerializeField] private TMP_Text playerScoreText;

    private void Awake()
    {
        MoveCount.DisplayMovesNumber += DisplayMoveNumber;

        ScoreCalculator.DisplayScore += DisplayPlayerScore;
    }

    private void OnDisable()
    {
        MoveCount.DisplayMovesNumber -= DisplayMoveNumber;

        ScoreCalculator.DisplayScore -= DisplayPlayerScore;
    }

    private void DisplayMoveNumber(int moveNumber)
    {
        playerMovesNumberText.text = moveNumber.ToString();
    }

    private void DisplayPlayerScore(int score)
    {
        playerScoreText.text = score.ToString();
    }

}
