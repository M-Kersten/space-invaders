using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalmTree : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float MaxLifeTime;
    [SerializeField]
    private float randomizeRotation;

    private float currentLifeTime;

    private void OnEnable() => currentLifeTime = 0;

    private void Update() => Move();

    /// <summary>
    /// Moves the bullet every frame
    /// </summary>
    private void Move()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed, Space.Self);
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > MaxLifeTime)
        {
            gameObject.SetActive(false);
            PalmTreePool.Instance.ReturnToPool(this);
        }
    }

    public void Initialize(Transform setTransform)
    {
        gameObject.SetActive(true);
        transform.GetChild(0).Rotate(Vector3.up, Random.Range(0, randomizeRotation));
        transform.position = setTransform.position;
        transform.localScale = new Vector3(0, 0, transform.localScale.z);
        LeanTween.scale(gameObject, Vector3.one, 1).setEase(LeanTweenType.easeInOutSine);
    }
}
