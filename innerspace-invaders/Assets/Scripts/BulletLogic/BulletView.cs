using UnityEngine;

/// <summary>
/// Class dedicated to visualizing changes in bullet logic
/// </summary>
public class BulletView : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private TrailRenderer trail;

    private void OnEnable() => bullet.Exploded += VisualizeExplosion;
    private void OnDisable() => bullet.Exploded -= VisualizeExplosion;

    /// <summary>
    /// Visualize explosion of the bullet attached
    /// </summary>
    private void VisualizeExplosion()
    {
        // clear the trailrenderer so it doesn't show streaks when re-initializing
        trail.Clear();
    }
}
