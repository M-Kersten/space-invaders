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
        {
            IBullet bullet = other.gameObject.GetComponent<IBullet>();
            if (bullet.BulletType == damagable.AffectedBulletType)
            {
                bullet.Explode();
                damagable.TakeDamage();
            }
        }
    }
}
