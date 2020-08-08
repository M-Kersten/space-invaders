using System;

public interface IDamagable
{
    int Health { get; }
    Action<int> HealtChanged { get; set; }
    BulletType AffectedBulletType { get; set; }
    void TakeDamage();
    void Die();    
}
