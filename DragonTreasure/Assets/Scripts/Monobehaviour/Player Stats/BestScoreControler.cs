using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreControler : MonoBehaviour
{
    private RecordData recordData = new RecordData();

    private int positionOfNewRecord;
    private int playerScore;

    public static Action NewRecordPlayerName;
    public static Action NoNewRecord;
    public static Action<RecordData> SaveGameData;

    private void Awake()
    {
        BestScoreInput.PlayerName += SetNewPlayerName;

        SaveLoad.GameEnd += SetRecordData;
    }

    private void OnDisable()
    {
        BestScoreInput.PlayerName -= SetNewPlayerName;

        SaveLoad.GameEnd -= SetRecordData;

    }
    private void TryGetPlayerName()
    {
        if (positionOfNewRecord >= 0) NewRecordPlayerName?.Invoke();
        else NoNewRecord?.Invoke();

    }

    private void SearchPositionToNewRecord(int newPlayerScore)
    {
        positionOfNewRecord = -1;
        for (int index = 0; index < recordData.Length; index++)
        {
            if (recordData.score[index] < newPlayerScore)
            {
                positionOfNewRecord = index;
                break;
            }
        }

        playerScore = newPlayerScore;
        TryGetPlayerName();
    }
    private void SetNewRecordToPosition(string playerName, int playerScore, int positionOfNewRecord)
    {
        ShiftRecordData(positionOfNewRecord);

        recordData.name[positionOfNewRecord] = playerName;
        recordData.score[positionOfNewRecord] = playerScore;

        SaveGameData?.Invoke(recordData);
    }

    private void ShiftRecordData(int startPosition)
    {
        for (int index = recordData.Length - 1; index > startPosition; index--)
        {
            recordData.name[index] = recordData.name[index - 1];
            recordData.score[index] = recordData.score[index - 1];
        }
    }
    private void SetNewPlayerName(string playerName)
    {
        SetNewRecordToPosition(playerName, playerScore, positionOfNewRecord);
    }

    private void SetRecordData(RecordData data, int score)
    {
        recordData = data;

        SearchPositionToNewRecord(score);
    }
}
