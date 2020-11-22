using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Cell : MonoBehaviour
{
    [Header("Является ли точкой для спавна взорванных шариков")]
    [SerializeField] private bool isSpawnPosition=false;

    private GameObject currentBubble = null;

    public GameObject CurrentBubble => currentBubble;
    public bool IsSpawnPosition => isSpawnPosition;

    private void Awake()
    {
        if (!isSpawnPosition) CreateBubble();
    }

    private void CreateBubble()
    {
        currentBubble = Instantiate(Resources.Load<GameObject>("Bubble Prefab/Bubble"), transform.position, transform.rotation);
    }

    public void RemoveCurrentBubble()
    {
        currentBubble.GetComponent<Bubble>().ExplosionBubble();

        ResetCurrentBubble();
    }

    public void ResetCurrentBubble()
    {
        currentBubble = null;
    }

    public void SetCurrentBubble(GameObject bubble)
    {
        currentBubble = bubble;
    }

    public void EmissionBubble()
    {
        currentBubble.GetComponent<Bubble>().DisplayFirstStep();
    }


}
