public class DefaultBullet : Bullet
{
    public override void Explode()
    {
        base.Explode();
        PlayerBulletPool.Instance.ReturnToPool(this);
    }
}
