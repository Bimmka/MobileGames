using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMover : MonoBehaviour
{
    [Header("Скорость падения шарика")]
    [SerializeField] private float moveSpeed;

    private float moveOffset = 0.05f;                                                       //насколько близко к конечной точке остановиться 
    private Vector3 targetCell;
    public static Func<Vector2, Vector2> GetSpawnPointPositionToExplosedBubble;

    private void Start()
    {
        targetCell = transform.position;
    }

    /// <summary>
    /// Перемещение взорванного шарика на spawnPoint
    /// </summary>
    public void MoveExplodedBubbleToNearestSpawnPoint()
    {
        Vector2 newBubblePosition = GetSpawnPointPositionToExplosedBubble(transform.position); //получить позицию спавна

        transform.position = newBubblePosition;
    }

    /// <summary>
    /// Метод для движения шарика к клетке
    /// </summary>
    /// <param name="cellPosition">Позиция клетки</param>
    public void MoveBubbleToCell(Vector2 cellPosition)
    {
        if (transform.position != targetCell) StopAllCoroutines();
        StartCoroutine(MoveToTarget(cellPosition));

    }

    private IEnumerator MoveToTarget(Vector2 target)
    {
        targetCell = target;
        while (transform.position.y - Time.deltaTime*moveSpeed > target.y)
        {
            transform.position = Vector2.Lerp(transform.position, transform.position - Vector3.up, Time.deltaTime * moveSpeed);
            yield return null;
        }
        transform.position = target;
    }
}
