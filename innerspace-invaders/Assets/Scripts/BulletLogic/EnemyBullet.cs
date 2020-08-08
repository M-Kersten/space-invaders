public class EnemyBullet : Bullet
{    
    public override void Explode()
    {
        base.Explode();
        EnemyBulletPool.Instance.ReturnToPool(this);
    }
}
