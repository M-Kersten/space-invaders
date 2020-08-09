using UnityEngine;

/// <summary>
/// Dedicated class for changing the visuals of an enemy
/// </summary>
[RequireComponent(typeof(Renderer))]
public class EnemyVisual : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;

    private Renderer renderer;

    private void Awake() => renderer = GetComponent<Renderer>();

    private void OnEnable() => enemy.ColorChanged += ChangeColor;

    private void OnDisable() => enemy.ColorChanged -= ChangeColor;

    /// <summary>
    /// update the enemy material color
    /// </summary>
    /// <param name="newColor"></param>
    private void ChangeColor(Color newColor)
    {
        renderer.material.color = newColor;
    }
}
