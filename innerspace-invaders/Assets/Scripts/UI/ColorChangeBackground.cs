using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Cycles through a list of colors and applies it to the image component of the gameobject
/// </summary>
[RequireComponent(typeof(Renderer))]
public class ColorChangeBackground : MonoBehaviour
{
    /// <summary>
    /// Duration on eacht transition between colors
    /// </summary>
    [SerializeField] private int fadeDuration;

    [SerializeField] private Color[] backgroundColors;

    private Renderer renderer;
    private int index;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        index = 0;
        renderer.material.SetColor("_Color", backgroundColors[0]);
        if (fadeDuration < .001f)
            return;

        LerpColors();
    }

    /// <summary>
    /// Continously lerp the colors of the background in a loop through all the backgroundcolors
    /// </summary>
    private void LerpColors()
    {
        LeanTween.value(renderer.gameObject, backgroundColors[index],
                backgroundColors[index + 1 >= backgroundColors.Length ? 0 : index + 1], fadeDuration)
            .setOnUpdate((value) => renderer.material.SetColor("_Color", value)).setOnComplete(() => LerpColors());
        index++;
        if (index >= backgroundColors.Length)
            index = 0;
    }
}