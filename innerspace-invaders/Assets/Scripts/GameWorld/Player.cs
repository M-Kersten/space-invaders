using System;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Player : StateBehaviour, IDamagable
{    
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
    public BulletType AffectedBulletType { get => affectedBulletType; set => affectedBulletType = value; }
    public Action<int> HealtChanged { get; set; }
    public Action<float> DirectionChanged { get; set; }

    [SerializeField]
    private BulletType affectedBulletType;
    [SerializeField]
    private BulletSpawner bulletSpawner;
    [SerializeField]
    private GameSettings settings;

    private Vector3 startingPosition;
    private int health;

    private void Start()
    {
        startingPosition = transform.position;
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

        float direction = Input.GetAxis(settings.Input.movementAxis);
        
        AddVelocity(direction);
        DirectionChanged?.Invoke(direction);

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

        transform.Translate(Vector3.right * Time.deltaTime * direction * settings.PlayerSpeed, Space.World);
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
        if ((oldState == GameState.Stopped || oldState == GameState.Lost | oldState == GameState.NextLevel) && state == GameState.Playing)
        {
            Health = settings.InitialPlayerHealth;
            transform.position = startingPosition;
        }
        CurrentState = state;
    }
}
