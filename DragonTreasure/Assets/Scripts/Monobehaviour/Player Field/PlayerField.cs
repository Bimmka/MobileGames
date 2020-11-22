using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerField : MonoBehaviour
{
    [Header("Куда вывести имя игрока")]
    [SerializeField] private TMP_Text playerNameText;

    [Header("Куда вывести счет игрока")]
    [SerializeField] private TMP_Text playerScoreText;

    public void SetPlayerRecord(string playerName, int playerScore)
    {
        playerNameText.text = playerName;
        playerScoreText.text = playerScore.ToString();
    }
}
