public class EnemyBullet : Bullet
{    
    public override void Explode()
    {
        EnemyBulletPool.Instance.ReturnToPool(this);
    }
}
