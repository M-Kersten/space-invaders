using UnityEngine;

/// <summary>
/// Dedicated class for changing the visuals of an enemy
/// </summary>
public class EnemyVisual : MonoBehaviour
{
    [SerializeField]
    private Enemy enemy;
    
    [SerializeField]
    private Renderer[] renderers;

    [SerializeField] 
    private ParticleSystem particles;

    [SerializeField]
    private Vector2 minMaxScaleAdjust;

    private void OnEnable()
    {
        enemy.ColorChanged += ChangeColor;
        enemy.EnemyKilled += VisualizeDeath;
        transform.localScale += Vector3.one * Random.Range(minMaxScaleAdjust.x, minMaxScaleAdjust.y);
    }

    private void OnDisable()
    {
        enemy.ColorChanged -= ChangeColor;
        enemy.EnemyKilled += VisualizeDeath;
    } 

    private void VisualizeDeath(Enemy enemy)
    {
        particles.Play();
        Destroy(particles.gameObject, particles.main.duration);
        particles.gameObject.transform.SetParent(particles.gameObject.transform.parent.parent, true);
        AudioManager.Instance.PlayClip(3);
    }
    
    /// <summary>
    /// update the enemy material color
    /// </summary>
    /// <param name="newColor"></param>
    private void ChangeColor(Color newColor)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = newColor;
        }
    }
}
