using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField]
    private BulletType bulletType;

    public void Shoot()
    {
        IBullet shot;
        if (bulletType == BulletType.Player)
            shot = PlayerBulletPool.Instance.GetPooledObject();        
        else        
            shot = EnemyBulletPool.Instance.GetPooledObject();

        // if the pool limit is reached (for example 5 for enemy shots like the challenge requirements mentioned) the shot will be null
        if (shot != null)
        {
            shot.Initialize(transform, bulletType);
            shot.Object.SetActive(true);
        }
    }

    /// <summary>
    /// Draw the firing direction in the gameworld to configure the spawning more easily
    /// </summary>
    private void DebugShootDirection()
    {
        Gizmos.color = bulletType == BulletType.Player ? Color.green : Color.red;
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 3;
        Gizmos.DrawRay(transform.position, direction);
    }
        
    private void OnDrawGizmos() => DebugShootDirection();
}
