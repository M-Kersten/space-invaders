using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField]
    private BulletSpawner bulletSpawner;
        
    [SerializeField]
    private GameSettings settings;

    [SerializeField]
    private Vector2 minMaxShotDelay;

    private float currentTime;
    private float shotDelay;

    public bool CurrentlyShooting;

    public int Health { get; private set; }
    public BulletType AffectedBulletType { get => returnBulletType; set => returnBulletType = value; }
    [SerializeField]
    private BulletType returnBulletType;

    private void Start()
    {
        shotDelay = Random.Range(minMaxShotDelay.x, minMaxShotDelay.y);
        Health = settings.InitialEnemyHealth;
        GetComponent<DamagableCollider>().Init(this);
    }

    private void Update()
    {
        if (!CurrentlyShooting)
            return;

        if (currentTime > shotDelay)
        {
            bulletSpawner.Shoot();
            currentTime = 0;
        }
        currentTime += Time.deltaTime;
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
