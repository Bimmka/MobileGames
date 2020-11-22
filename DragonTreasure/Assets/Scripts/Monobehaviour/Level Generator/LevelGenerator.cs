using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("GameObject, который будет Родителем для точек спавна")]
    [SerializeField] private Transform spawPointsParentTransform;

    private TextAsset levelGeneratorText;                                                       //файл .txt, в котором хранится уровень

    private int stringNumber;                                                                   //количество строк на уровне
    private int rowNumber;                                                                      //количество столбцов на уровне
    

    private Cell[,] cellGrid;                                                                   //двумерная матрица уровня

    private GameObject[] spawnPoints;
    private GameObject cell;                                                                    //префаб клетки
    private GameObject spawnPoint;


    public static Action<GameObject[]> SetSpawnPoints;
    public static Action<Cell[,]> SetCellGrid;
    private void Awake()
    {
        levelGeneratorText = Resources.Load<TextAsset>("Level Generator Texts/Level1");

        cell = Resources.Load<GameObject>("Cell Prefab/Cell");
        spawnPoint = Resources.Load<GameObject>("Cell Prefab/Spawn Point");

        ReadTextGeneratorFile();
    }

    private void Start()
    {
        SetGeneratedValues();
    }


    private void ReadTextGeneratorFile()
    {    
        string[] readedLinesFromTextGenerator = levelGeneratorText.text.Split('\n');

        SetCellGridRowsAndStrings(readedLinesFromTextGenerator[0]);

        GenerateLevel(readedLinesFromTextGenerator);

        
    }

    private void SetCellGridRowsAndStrings(string readedString)
    {
        string[] splitedString = readedString.Split(',');

        if (splitedString.Length == 2)
        {
            stringNumber = Convert.ToInt32(splitedString[0]);
            rowNumber = Convert.ToInt32(splitedString[1]);

            cellGrid = new Cell[stringNumber, rowNumber];
            spawnPoints = new GameObject[rowNumber];
        }
        else Debug.LogError("Неверный формат текстового документа для создания уровня");

          
    }

    private void GenerateLevel(string[] readedLinesFromTextGenerator)
    {

        for (int indexReadedLine = 1; indexReadedLine < readedLinesFromTextGenerator.Length; indexReadedLine++)             //indexReadedLine - индекс в массиве считанных строк из файла
        {                                                                                                                   //а так же на 1 увеличенное значение номера строки в cellGrid
            string[] splitedString = readedLinesFromTextGenerator[indexReadedLine].Split(',');           
            for (int rowIndexInCellGrid = 0; rowIndexInCellGrid < rowNumber; rowIndexInCellGrid++)                          //rowIndexInCellGrid - индекс столбца в cellGrid
            {                                                                                                               //а так же индекс в splitedString
                if (splitedString[rowIndexInCellGrid] == "1" || splitedString[rowIndexInCellGrid] == "1\r")
                    cellGrid[indexReadedLine-1, rowIndexInCellGrid] = Instantiate(cell, transform.position - new Vector3(-rowIndexInCellGrid, indexReadedLine - 1, 0), transform.rotation, transform).GetComponent<Cell>();

                else if (splitedString[rowIndexInCellGrid] == "0" || splitedString[rowIndexInCellGrid] == "0\r")
                {
                    GameObject point = Instantiate(spawnPoint, transform.position - new Vector3(-rowIndexInCellGrid, indexReadedLine - 1, 0), transform.rotation, spawPointsParentTransform);
                    cellGrid[indexReadedLine-1, rowIndexInCellGrid] = point.GetComponent<Cell>();
                    spawnPoints[rowIndexInCellGrid] = point;
                }                        
            }
            
        }        
    }

    private void SetGeneratedValues()
    {
        if (SetSpawnPoints != null) SetSpawnPoints(spawnPoints);

        if (SetCellGrid != null) SetCellGrid(cellGrid);
    }


}
