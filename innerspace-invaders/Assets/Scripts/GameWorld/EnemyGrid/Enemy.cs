using System;
using UnityEngine;
using Random = UnityEngine.Random;

[SelectionBase]
[RequireComponent(typeof(DamagableCollider))]
public class Enemy : MonoBehaviour, IDamagable
{
    public Color EnemyColor { get { return enemyColor; } private set { enemyColor = value; ColorChanged?.Invoke(enemyColor); } }
    public Vector2Int Index { get; private set; }
    public bool CurrentlyShooting;
    public bool ShootingEnabled;

    public int Health
    {
        get { return health; }
        private set
        {
            health = value;
            HealtChanged?.Invoke(health);
            if (Health <= 0)
                Die();
        }
    }
    private int health;

    public BulletType AffectedBulletType { get => affectedBulletType; set => affectedBulletType = value; }

    public Action<int> HealtChanged { get; set; }
    public Action<Color> ColorChanged;
    public Action<Enemy> EnemyKilled;


    [SerializeField]
    private BulletType affectedBulletType;

    [SerializeField]
    private BulletSpawner bulletSpawner;
        
    [SerializeField]
    private Vector2 minMaxShotDelay;

    private float currentTime;
    private float shotDelay;
    private Color enemyColor;

    private void Start()
    {        
        // initialize the collider attached to this enemy
        GetComponent<DamagableCollider>().Init(this);
        shotDelay = Random.Range(minMaxShotDelay.x, minMaxShotDelay.y);
        ShootingEnabled = true;
    }

    private void Update()
    {
        if (!CurrentlyShooting || !ShootingEnabled)
            return;

        if (currentTime > shotDelay)
        {
            bulletSpawner.Shoot();
            currentTime = 0;
            shotDelay = Random.Range(minMaxShotDelay.x, minMaxShotDelay.y);
        }
        currentTime += Time.deltaTime;
    }

    public void Initialize(Vector2Int newIndex, LevelSettings settings)
    {
        Index = newIndex;        
        Health = settings.InitialEnemyHealth;
        EnemyColor = settings.EnemyColors[Random.Range(0, settings.EnemyColors.Length)];
        gameObject.name = $"enemy (x:{newIndex.x}, y:{newIndex.y})";
    }

    public void Die()
    {
        Destroy(gameObject);
        EnemyKilled?.Invoke(this);
    }

    public void TakeDamage()
    {
        Health--;        
    }
}
