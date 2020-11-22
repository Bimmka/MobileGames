using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodedBubbleSetter : MonoBehaviour
{
    private Cell[,] cellGrid;

    private List<int> rowIndexesOfExplodedBubbles = new List<int>();

    private Queue<Cell> queueOfCellsWithoutBubbles = new Queue<Cell>();

    private int stringCountInCellGrid;

    public static Func<Vector2, GameObject> GetBubbleByPosition;

    private void Awake()
    {
        LevelGenerator.SetCellGrid += SetSellGrid;

        BubbleGrid.RowIndexOfExlpodedBubbles += SetRowIndexesOfExplodedBubbles;
    }

    private void OnDisable()
    {
        LevelGenerator.SetCellGrid -= SetSellGrid;

        BubbleGrid.RowIndexOfExlpodedBubbles -= SetRowIndexesOfExplodedBubbles;
    }

    /// <summary>
    /// Получаем экзепляр игровой решетки
    /// </summary>
    /// <param name="grid">Решетка, которая сгенерирована LevelGenerator</param>
    private void SetSellGrid(Cell[,] grid)
    {
        cellGrid = grid;

        stringCountInCellGrid = cellGrid.GetLength(0);
    }

    /// <summary>
    /// Получаем индексы столбцов, в которых взорвались шарики
    /// </summary>
    /// <param name="indexesOfExplodedBubbles">Индексы столбцов со взорванными шариками</param>
    private void SetRowIndexesOfExplodedBubbles(List<int> indexesOfExplodedBubbles)
    {
        rowIndexesOfExplodedBubbles = indexesOfExplodedBubbles;

        MoveBubbleToEmptyCell();

        queueOfCellsWithoutBubbles.Clear();
    }

    /// <summary>
    /// Заполнение пустых клеток
    /// </summary>
    private void MoveBubbleToEmptyCell()
    {

        foreach (var rowIndex in rowIndexesOfExplodedBubbles)
        {
            for (int stringIndex = stringCountInCellGrid - 1; stringIndex > 0; stringIndex--)
            {
                GameObject bubble = cellGrid[stringIndex, rowIndex].CurrentBubble;

                if (bubble == null) queueOfCellsWithoutBubbles.Enqueue(cellGrid[stringIndex, rowIndex]);
                else
                {
                    if (queueOfCellsWithoutBubbles.Count > 0)
                        MoveBubbleToCell(bubble, cellGrid[stringIndex, rowIndex], queueOfCellsWithoutBubbles.Dequeue());
                }
            }
            MoveExplodedBubblesToCell();
        }
    }

    /// <summary>
    /// Движение шарика к клетке
    /// </summary>
    /// <param name="bubble">Какой шарик передвинуть</param>
    /// <param name="fromCell">С какой клетки</param>
    /// <param name="toCell">В какую клетку</param>
    private void MoveBubbleToCell(GameObject bubble, Cell fromCell, Cell toCell)
    {        
        bubble.GetComponent<Bubble>().SetBubbleToCell(toCell.transform.position);

        fromCell.ResetCurrentBubble();
        toCell.SetCurrentBubble(bubble);

        queueOfCellsWithoutBubbles.Enqueue(fromCell);
    }

    private void MoveExplodedBubblesToCell()
    {
        while (queueOfCellsWithoutBubbles.Count > 0)
        {
            SetExplodedBubblesToCell(queueOfCellsWithoutBubbles.Dequeue());
        }
    }

    private void SetExplodedBubblesToCell(Cell toCell)
    {
        GameObject bubble = GetBubbleByPosition(toCell.transform.position);

        bubble.GetComponent<Bubble>().SetExplodedBubbleToCell(toCell.transform.position);

        toCell.SetCurrentBubble(bubble);
    }
}
