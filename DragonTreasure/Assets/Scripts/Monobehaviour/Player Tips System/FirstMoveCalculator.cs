using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstMoveCalculator : MonoBehaviour
{
    private Cell[,] cellGrid;
    private List<Cell> listGrid = new List<Cell>();
    private List<Cell> listNearestCellWitnOneColorBubble = new List<Cell>();

    private Cell firstCell;

    private int maxBubbleCount = 0;

    private int rowCountInCellGrid;
    private int stringCountInCellGrid;

    public static Action<Vector3> FirstMoveCell;
    private void Awake()
    {
        BubbleGrid.SetListGrid += SetListGrid;
        TipsDisplay.DisplayFirstMove += SearchBestFirstMove;
    }

    private void OnDisable()
    {
        BubbleGrid.SetListGrid -= SetListGrid;
        TipsDisplay.DisplayFirstMove -= SearchBestFirstMove;
    }

    private void SetListGrid(List<Cell> cellList, Cell[,] grid, int stringCount, int rowCount)
    {
        cellGrid = grid;
        listGrid = cellList;
        stringCountInCellGrid = stringCount;
        rowCountInCellGrid = rowCount;
    }

    /// <summary>
    /// Поиск клетки, по которой кликнули
    /// </summary>
    /// <param></param>
    private void SearchBestFirstMove()
    {
        foreach (var cell in listGrid)
        {
            int cellIndex = listGrid.IndexOf(cell);
            SearchCellsWithOneColorBubble(cellIndex % rowCountInCellGrid, cellIndex / rowCountInCellGrid, cell.CurrentBubble.GetComponent<Bubble>().BubbleColor, cell);
        }

        FirstMoveCell?.Invoke(firstCell.transform.position);
        firstCell.GetComponent<Cell>().EmissionBubble();
    }

    /// <summary>
    /// Поиск клеток, на которых находятся шарики схожего цвета
    /// </summary>
    /// <param name="cellRowIndex">Индекс по столбцу в сетке клеток</param>
    /// <param name="cellSrtingIndex">Индекс по строке в сетке клеток</param>
    /// <param name="bubbleColor">Цвет шарика</param>
    /// <param name="clickedCell">Кликнутая клетка</param>
    private void SearchCellsWithOneColorBubble(int cellRowIndex, int cellSrtingIndex, string bubbleColor, Cell clickedCell)
    {        
        listNearestCellWitnOneColorBubble.Add(clickedCell);                                                     //лист с клетками, на котором одинакоые по цвету шарики

        SearchCellWithOneColorByDirection(cellRowIndex, cellSrtingIndex, bubbleColor, rowCountInCellGrid, "Horizontal", 1 );
        SearchCellWithOneColorByDirection(cellRowIndex, cellSrtingIndex, bubbleColor, rowCountInCellGrid, "Horizontal", -1);
        SearchCellWithOneColorByDirection(cellSrtingIndex, cellRowIndex, bubbleColor, stringCountInCellGrid - 1, "Vertical", 1);
        SearchCellWithOneColorByDirection(cellSrtingIndex, cellRowIndex, bubbleColor, stringCountInCellGrid - 1, "Vertical", -1);

        SetMaxBubblesCount();
    }

    /// <summary>
    /// Поиск клеток с шариками, одинакового цвета
    /// </summary>
    /// <param name="varibaleParametr">Параметр, по которому передвигаемся</param>
    /// <param name="immutableParametr">Параметр, который имеет постоянное значение</param>
    /// <param name="bubbleColor">Цвет шарика</param>
    /// <param name="maxValue">Максимальное значение для меняющегося параметра</param>
    /// <param name="direction">Направление движения (Horizontal, Vertical)</param>
    /// <param name="offset">Сдвиг, отвечает за джвиение вперед/назад по направлению (1 - впередб -1 - назад)</param>
    private void SearchCellWithOneColorByDirection(int varibaleParametr, int immutableParametr, string bubbleColor, int maxValue, string direction, int offset)
    {
        bool isBubbleOneColor = true;
        while (isBubbleOneColor)
        {
            varibaleParametr += offset;
            if (CheckNumberInRange(varibaleParametr, 0, maxValue))
            {
                if (direction == "Horizontal")
                {
                    if (CheckNearestCellWithBubbleForColor(varibaleParametr, immutableParametr, bubbleColor))
                    {
                        AddNearestCellWithBubbleForColor(varibaleParametr, immutableParametr);
                    }
                    else isBubbleOneColor = false;
                }
                else if (direction == "Vertical")
                {
                    if (CheckNearestCellWithBubbleForColor(immutableParametr, varibaleParametr, bubbleColor))
                    {
                        AddNearestCellWithBubbleForColor(immutableParametr, varibaleParametr);
                    }
                    else isBubbleOneColor = false;
                }
            }
            else isBubbleOneColor = false;
        }
    }

    /// <summary>
    /// Проверяем, что число в допустимом диапазоне
    /// </summary>
    /// <param name="number">Значения числа</param>
    /// <param name="minValue">Минимальное значение</param>
    /// <param name="maxValue">Максимальное значение (не включая)</param>
    /// <returns></returns>
    private bool CheckNumberInRange(int number, int minValue, int maxValue)
    {
        return number >= minValue && number < maxValue;
    }

    /// <summary>
    /// Проверяем, что шарик на другой клетке схож по цвету с нашим
    /// </summary>
    /// <param name="cellRowIndex">Индекс по столбцу в сетке клеток</param>
    /// <param name="cellSrtingIndex">Индекс по строке в сетке клеток</param>
    /// <param name="bubbleColor">Цвет шарика</param>
    /// <returns></returns>
    private bool CheckNearestCellWithBubbleForColor(int cellRowIndex, int cellSrtingIndex, string bubbleColor)
    {

        GameObject bubble = cellGrid[cellSrtingIndex + 1, cellRowIndex].CurrentBubble;       // +1, потому что cellGrid на строчку больше, т.к. хранит SpawnPoint
        if (bubble != null)
            return bubble.GetComponent<Bubble>().BubbleColor == bubbleColor;
        else return false;
    }

    /// <summary>
    /// Добавляем клетки, которые у которых шарики одинакового цвета
    /// </summary>
    /// <param name="cellRowIndex"></param>
    /// <param name="cellSrtingIndex"></param>
    private void AddNearestCellWithBubbleForColor(int cellRowIndex, int cellSrtingIndex)
    {
        listNearestCellWitnOneColorBubble.Add(cellGrid[cellSrtingIndex + 1, cellRowIndex]);                               // +1, потому что cellGrid на строчку больше, т.к. хранит SpawnPoint
    }

    private void SetMaxBubblesCount()
    {
        if (listNearestCellWitnOneColorBubble.Count > maxBubbleCount)
        {
            maxBubbleCount = listNearestCellWitnOneColorBubble.Count;
            firstCell = listNearestCellWitnOneColorBubble[0];
        }
        listNearestCellWitnOneColorBubble.Clear();
    }
}
