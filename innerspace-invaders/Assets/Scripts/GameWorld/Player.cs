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
        InputManager.Instance.processMove += AddVelocity;
        InputManager.Instance.processShoot += bulletSpawner.Shoot;
        startingPosition = transform.position;
        GetComponent<DamagableCollider>().Init(this);
    }

    /// <summary>
    /// Move the player along the x axis
    /// </summary>
    /// <param name="direction"></param>
    private void AddVelocity(float direction)
    {
        DirectionChanged(direction);
        // check for out of bounds
        if ((transform.position.x > settings.PlayerBounds.y && direction > 0) || (transform.position.x < settings.PlayerBounds.x && direction < 0))
            return;

        transform.Translate(Vector3.right * Time.deltaTime * direction * settings.PlayerSpeed, Space.World);
    }

    public void TakeDamage()
    {
        if (CurrentState == GameState.Playing)
        {
            Health--;
            VibrationManager.VibrateError();
        }
    }

    public void Die()
    {
        SetState?.Invoke(GameState.Lost);
    }

    public override void UpdateState(GameState state, GameState oldState)
    {
        AudioManager.Instance.PlayClip(0);
        if ((oldState == GameState.Stopped || oldState == GameState.Lost | oldState == GameState.NextLevel) && state == GameState.Playing)
        {
            Health = settings.InitialPlayerHealth;
            transform.position = startingPosition;
        }
        CurrentState = state;
    }
}
