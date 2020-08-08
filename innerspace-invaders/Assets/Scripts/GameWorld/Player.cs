using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Player : StateBehaviour, IDamagable
{    
    [SerializeField]
    private BulletSpawner bulletSpawner;
    [SerializeField]
    private GameSettings settings;

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
    private float speed;

    public BulletType AffectedBulletType { get => affectedBulletType; set => affectedBulletType = value; }

    public Action<int> HealtChanged { get; set; }

    [SerializeField]
    private BulletType affectedBulletType;

    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
        speed = settings.PlayerSpeed;
        GetComponent<DamagableCollider>().Init(this);
    }

    private void Update() => ProcessInput();

    /// <summary>
    /// Gets the user input and acts accordingly
    /// </summary>
    private void ProcessInput()
    {
        if (CurrentState != GameState.Playing)
            return;

        AddVelocity(Input.GetAxis(settings.Input.movementAxis));

        if (Input.GetKeyDown(settings.Input.ShootButton))
            bulletSpawner.Shoot();
    }

    /// <summary>
    /// Move the player along the x axis
    /// </summary>
    /// <param name="direction"></param>
    private void AddVelocity(float direction)
    {
        // check for out of bounds
        if ((transform.position.x > settings.PlayerBounds.y && direction > 0) || (transform.position.x < settings.PlayerBounds.x && direction < 0))
            return;

        transform.Translate(Vector3.right * Time.deltaTime * direction * speed, Space.Self);
    }

    public void TakeDamage()
    {
        Health--;        
    }

    public void Die()
    {
        SetState?.Invoke(GameState.Lost);
    }

    public override void UpdateState(GameState state, GameState oldState)
    {
        if ((CurrentState == GameState.Stopped || CurrentState == GameState.Lost) && state == GameState.Playing)
        {
            Health = settings.InitialPlayerHealth;
            transform.position = startingPosition;
        }
        CurrentState = state;
    }
}
