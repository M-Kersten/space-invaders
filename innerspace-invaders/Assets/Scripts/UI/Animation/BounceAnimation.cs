using UnityEngine;

/// <summary>
/// Script for scaling a gameobject in a smooth repeating fashion
/// </summary>
public class BounceAnimation : MonoBehaviour
{
    /// <summary>
    /// Scale to animate towards and from
    /// </summary>
    [SerializeField]
    private float scale = .9f;

    private void OnEnable()
    {
        LeanTween.scale(gameObject, Vector3.one * scale, Random.Range(1.5f, 2f))
            .setEase(LeanTweenType.easeInOutSine)
            .setDelay(Random.Range(0, 1))
            .setLoopPingPong();
    }

    private void OnDisable() => LeanTween.cancel(gameObject);
}