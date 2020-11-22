using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExplodedBubbleCollector : MonoBehaviour
{
    private List<GameObject> explodedBubbles = new List<GameObject>();

    public static Action<int> ExplodedBubbles;
    private void Awake()
    {
        ExplodedBubbleSetter.GetBubbleByPosition += GetExplodedBubblesByPosition;

        BubbleGrid.RemoveExplodedBubbles += SetExplodedBubble;
    }

    private void OnDisable()
    {
        ExplodedBubbleSetter.GetBubbleByPosition -= GetExplodedBubblesByPosition;

        BubbleGrid.RemoveExplodedBubbles -= SetExplodedBubble;
    }

    private void SetExplodedBubble (List<GameObject> bubbles)
    {
        explodedBubbles = bubbles;

        ExplodedBubbles(explodedBubbles.Count);
    }

    private GameObject GetExplodedBubblesByPosition(Vector2 cellPosition)
    {
        GameObject explodedBubble = explodedBubbles.FirstOrDefault(bubble => bubble.transform.position.x == cellPosition.x);
        explodedBubbles.Remove(explodedBubble);
        return explodedBubble;
    }

}
