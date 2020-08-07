public class DefaultBullet : Bullet
{
    public override void Explode()
    {
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}
