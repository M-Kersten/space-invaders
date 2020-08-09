using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IBullet
{
    public Action Exploded;    
    public GameObject Object => gameObject;
    public BulletType BulletType { get; private set; }
    public float MaxLifeTime;

    [SerializeField]
    private float speed;
    
    private float currentLifeTime;

    private void OnEnable() => currentLifeTime = 0;

    private void Update() => Move();

    /// <summary>
    /// Moves the bullet every frame
    /// </summary>
    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > MaxLifeTime)
            Explode();        
    }

    /// <summary>
    /// Initialize the bullet at a new position and type
    /// </summary>
    public void Initialize(Transform setTransform, BulletType type)
    {
        transform.position = setTransform.position;
        transform.rotation = setTransform.rotation;
        BulletType = type;
    }    

    /// <summary>
    /// Invoke the explode action
    /// </summary>
    public virtual void Explode() => Exploded?.Invoke();
}
