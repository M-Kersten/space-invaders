using UnityEngine;

public interface IBullet
{    
    void Move();
    void Explode();
    void Initialize(Transform setTransform, BulletType type);
    GameObject Object { get; }
    BulletType BulletType { get; }
}
