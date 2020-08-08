using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Wall : StateBehaviour, IDamagable
{
    [SerializeField]
    private GameSettings settings;

    public int Health {
        get { return health; } 
        private set 
        { 
            health = value;
            HealtChanged?.Invoke(health);
            if (health <= 0)
                Die();
        } 
    }
    private int health;
    public BulletType AffectedBulletType { get => affectedBulletType; set => affectedBulletType = value; }

    public Action<int> HealtChanged { get; set; }

    [SerializeField]
    private BulletType affectedBulletType;

    private void Start()
    {
        GetComponent<DamagableCollider>().Init(this);
    }

    public void Die()
    {
        LeanTween.moveLocalY(gameObject, -2, .5f).setEase(LeanTweenType.easeInOutQuart);
    }

    public void TakeDamage()
    {
        Health--;
    }

    public override void UpdateState(GameState state, GameState oldState)
    {
        if ((CurrentState == GameState.Stopped || CurrentState == GameState.Lost) && state == GameState.Playing)
        {
            transform.localPosition += Vector3.down * 2;
            LeanTween.moveLocalY(gameObject, 0, .5f).setEase(LeanTweenType.easeInOutQuart);
            Health = settings.WallHealth;
        }
        
        CurrentState = state;
    }
}
