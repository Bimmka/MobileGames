using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BubbleColors  { RED, GREEN, BLUE, YELLOW, PURPLE};

[RequireComponent(typeof(BubbleMover))]
public class Bubble : MonoBehaviour
{
    [Header("Bubble VFX")]
    [SerializeField] private SpriteRenderer bubbleSpriteRenderer;

    [Header("Звук взрыва")]
    [SerializeField] private AudioSource explosionSound;

    [Header("Эффект для первого хода")]
    [SerializeField] private ParticleSystem firstStepEffect;

    private BubbleColors bubbleColor = new BubbleColors();                    //Возможные цвета шарика

    private int maxBubbleColors = 5;                                        //Максимальное количество цветов шарика

    private BubbleMover bubbleMover;

    public static Func<string, Sprite> GetSpriteByBubbleColor;              //Функция для получения sprite шарика в завиимости от цвета шарика

    public string BubbleColor => bubbleColor.ToString();

    private void Start()
    {
        SetNewBubbleColorAndFVX();
        bubbleMover = GetComponent<BubbleMover>();
    }

    /// <summary>
    /// Метод для выставления рандомного цвета шарика
    /// </summary>
    private void SetNewBubbleColor()
    {
        int randomColorIndex = UnityEngine.Random.Range(0, maxBubbleColors);

        bubbleColor = (BubbleColors)randomColorIndex;
    }

    /// <summary>
    /// Метод для выставления sprite шарика в зависимости от его цвета
    /// </summary>
    private void SetBubbleSpriteByColor()
    {
        bubbleSpriteRenderer.sprite = GetSpriteByBubbleColor(bubbleColor.ToString());
    }

    private void SetBubbleSpriteAlpha(float value)
    {
        Color intermediateColor = bubbleSpriteRenderer.color;
        intermediateColor.a = value;
        bubbleSpriteRenderer.color = intermediateColor;
    }

    /// <summary>
    /// Метод для выставления нового цвета и sprite шарика
    /// </summary>
    public void SetNewBubbleColorAndFVX()
    {
        SetNewBubbleColor();
        SetBubbleSpriteByColor();
    }

    public void ExplosionBubble()
    {
        explosionSound.Play();
        if (firstStepEffect.isPlaying) firstStepEffect.Stop();

        SetBubbleSpriteAlpha(0f);
        SetNewBubbleColorAndFVX();
        bubbleMover.MoveExplodedBubbleToNearestSpawnPoint();
    }

    public void SetExplodedBubbleToCell(Vector2 cellPosition)
    {
        SetBubbleSpriteAlpha(1f);
        bubbleMover.MoveBubbleToCell(cellPosition);
    }

    public void SetBubbleToCell(Vector2 cellPosition)
    {
        bubbleMover.MoveBubbleToCell(cellPosition);
    }

    public void DisplayFirstStep()
    {
        firstStepEffect.Play();
    }

}
