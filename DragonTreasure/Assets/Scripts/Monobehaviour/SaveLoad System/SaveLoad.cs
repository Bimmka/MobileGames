using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoad: MonoBehaviour
{

    private RecordData recordData = new RecordData();
    private string path;

    public static Action<RecordData, int> GameEnd;
    private void Awake()
    {
        BestScoreControler.SaveGameData += SaveGame;

        ScoreCalculator.EndGame += GameOver;
    }

    private void OnDisable()
    {
        BestScoreControler.SaveGameData -= SaveGame;

        ScoreCalculator.EndGame -= GameOver;
    }


    private void Start()
    {
#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (!File.Exists(path)) CreateDefaultSaveData();
        LoadGame();
    }

    /// <summary>
    /// Создание .json файла
    /// </summary>
    private void CreateDefaultSaveData()
    {
        for (int index = 0; index < recordData.name.Length; index++)
        {
            recordData.name[index] = "Empty";
            recordData.score[index] = 0;
        }
        DefaultSaveGame();
    }

    /// <summary>
    /// Сохраняем дефолтные значения
    /// </summary>
    private void DefaultSaveGame()
    {
        File.WriteAllText(path, JsonUtility.ToJson(recordData));
    }

    /// <summary>
    /// Сохраняем новые значения RecordData
    /// </summary>
    /// <param name="data"></param>
    private void SaveGame(RecordData data)
    {
        recordData = data;
        File.WriteAllText(path, JsonUtility.ToJson(recordData));

    }

    /// <summary>
    /// Загружаем сохраненные данные
    /// </summary>
    private void LoadGame()
    {
        recordData = JsonUtility.FromJson<RecordData>(File.ReadAllText(path));
    }

    private void GameOver(int playerScore)
    {
        GameEnd?.Invoke(recordData, playerScore);
    }
}


[Serializable]
public class RecordData
{
    private static int numberOfPlayers = 10;

    public string[] name = new string[numberOfPlayers];
    public int[] score = new int[numberOfPlayers];

    public int Length => numberOfPlayers;
}