using System.IO;
using UnityEngine;

public class LoadScores : MonoBehaviour
{
    [Header("Куда помещать рекорды игроков")]
    [SerializeField] private Transform contentViewTransform;

    private RecordData recordData = new RecordData();
    private string path;

    private GameObject playerField;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
        path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        path = Path.Combine(Application.dataPath, "Save.json");
#endif
        playerField = Resources.Load<GameObject>("Player Score Field/Player Score Field");
        if (!File.Exists(path)) CreateDefaultSaveData();
        LoadGame();
    }
    private void CreateDefaultSaveData()
    {
        for (int index = 0; index < recordData.name.Length; index++)
        {
            recordData.name[index] = "Empty";
            recordData.score[index] = 0;
        }
        SaveGame();
    }

    private void SaveGame()
    {
        File.WriteAllText(path, JsonUtility.ToJson(recordData));
    }

    private void LoadGame()
    {
        recordData = JsonUtility.FromJson<RecordData>(File.ReadAllText(path));

        DisplayPlayersRecord();
    }

    private void DisplayPlayersRecord()
    {
        for (int index = 0; index < recordData.Length; index++)
        {
            if (recordData.name[index] != "Empty") CreatePlayerField(recordData.name[index], recordData.score[index]);
            else break;
        }
    }

    private void CreatePlayerField (string playerName, int playerScore)
    {
        GameObject instantiedPlayerField = Instantiate(playerField, contentViewTransform);
        instantiedPlayerField.GetComponent<PlayerField>().SetPlayerRecord(playerName, playerScore);
    }
}
