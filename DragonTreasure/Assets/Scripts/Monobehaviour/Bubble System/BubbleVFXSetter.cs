using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleVFXSetter : MonoBehaviour
{
    #region Спрайты для цветов шарика

    [Header("Спрайта для зеленого шарика")]
    [SerializeField] private Sprite greenBubbleVFX;

    [Header("Спрайта для красного шарика")]
    [SerializeField] private Sprite redBubbleVFX;

    [Header("Спрайта для синего шарика")]
    [SerializeField] private Sprite blueBubbleVFX;

    [Header("Спрайта для желтого шарика")]
    [SerializeField] private Sprite yellowBubbleVFX;

    [Header("Спрайта для фиолетовго шарика")]
    [SerializeField] private Sprite purpleBubbleVFX;

    #endregion

    private Dictionary<string, Sprite> colorVFXDictionary = new Dictionary<string, Sprite>();

    private void OnEnable()
    {
        colorVFXDictionary.Add("BLUE", blueBubbleVFX);
        colorVFXDictionary.Add("RED", redBubbleVFX);
        colorVFXDictionary.Add("GREEN", greenBubbleVFX);
        colorVFXDictionary.Add("YELLOW", yellowBubbleVFX);
        colorVFXDictionary.Add("PURPLE", purpleBubbleVFX);

        Bubble.GetSpriteByBubbleColor += GetSpriteByColor;
    }

    private void OnDisable()
    {
        Bubble.GetSpriteByBubbleColor -= GetSpriteByColor;
    }

    public Sprite GetSpriteByColor (string color)
    {
        return colorVFXDictionary[color];
    }
}
