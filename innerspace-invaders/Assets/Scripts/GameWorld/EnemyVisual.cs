using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class EnemyVisual : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private Renderer renderer;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        enemy.ColorChanged += ChangeColor;
    }

    private void OnDisable()
    {
        enemy.ColorChanged -= ChangeColor;
    }

    private void ChangeColor(Color newColor)
    {
        renderer.material.color = newColor;
    }
}
