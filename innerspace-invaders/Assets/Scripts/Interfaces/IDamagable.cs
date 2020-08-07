public interface IDamagable
{
    int Health { get; }
    BulletType AffectedBulletType { get; set; }
    void TakeDamage();
    void Die();    
}
