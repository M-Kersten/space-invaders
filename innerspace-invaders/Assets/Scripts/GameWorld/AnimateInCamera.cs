using UnityEngine;

/// <summary>
/// Animates camera rotation on enable
/// </summary>
public class AnimateInCamera : MonoBehaviour
{

    [SerializeField]
    private Vector3 startRotation;
    [SerializeField]
    private Vector3 endRotation;
    [SerializeField]
    private float animationDuration;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(startRotation);
        LeanTween.rotate(gameObject, endRotation, animationDuration).setEase(LeanTweenType.easeInOutQuart);
    }
}
