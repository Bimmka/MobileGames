using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointsCollector : MonoBehaviour
{
    private GameObject[] spawnPoints;

    private void OnEnable()
    {
        LevelGenerator.SetSpawnPoints += SetSpawnPoints;
    }

    private void Start()
    {
        BubbleMover.GetSpawnPointPositionToExplosedBubble += GetSpawnPointPositionByBubblePosition;


    }

    private void OnDisable()
    {
        LevelGenerator.SetSpawnPoints -= SetSpawnPoints;

        BubbleMover.GetSpawnPointPositionToExplosedBubble -= GetSpawnPointPositionByBubblePosition;
    }

    private void SetSpawnPoints(GameObject[] points)
    {
        spawnPoints = new GameObject[points.Length];

        spawnPoints = points;
    }

    private Vector2 GetSpawnPointPositionByBubblePosition(Vector2 bubblePosition)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.transform.position.x == bubblePosition.x) return spawnPoint.transform.position;
        }
        return spawnPoints[0].transform.position;
    }
}
