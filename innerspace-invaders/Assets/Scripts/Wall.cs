using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Wall : MonoBehaviour, IDamagable
{
    [SerializeField]
    private GameSettings settings;

    public int Health { get; private set; }
    public BulletType AffectedBulletType { get => affectedBulletType; set => affectedBulletType = value; }
    [SerializeField]
    private BulletType affectedBulletType;

    private void Start()
    {
        Health = settings.WallHealth;
        GetComponent<DamagableCollider>().Init(this);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage()
    {
        Health--;
        if (Health <= 0)
            Die();
    }
}
