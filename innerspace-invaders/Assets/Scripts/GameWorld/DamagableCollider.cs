using Invaders.Tools;
using UnityEngine;

/// <summary>
/// Dedicated class to handle bullet to damagable collisions
/// </summary>
[RequireComponent(typeof(Collider))]
public class DamagableCollider : MonoBehaviour
{
    private IDamagable damagable;

    public void Init(IDamagable newDamagable) => damagable = newDamagable;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.BULLET))
            BulletHitDetected(other);
    }

    /// <summary>
    /// When a bullet hit is detected, check whether the damagable is detecting the bullet type and act accordingly
    /// </summary>
    /// <param name="other"></param>
    private void BulletHitDetected(Collider other)
    {
        IBullet bullet = other.gameObject.GetComponent<IBullet>();
        if (bullet.BulletType == damagable.AffectedBulletType)
        {
            bullet.Explode();
            damagable.TakeDamage();            
        }
    }
}
