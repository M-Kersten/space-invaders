using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DamagableCollider))]
public class Player : MonoBehaviour, IDamagable
{    
    [SerializeField]
    private BulletSpawner bulletSpawner;
    [SerializeField]
    private GameSettings settings;
    
    public int Health { get; private set; }
    private float speed;

    public BulletType AffectedBulletType { get => returnBulletType; set => returnBulletType = value; }
    [SerializeField]
    private BulletType returnBulletType;

    private void Start()
    {
        Health = settings.InitialPlayerHealth;
        speed = settings.PlayerSpeed;
        GetComponent<DamagableCollider>().Init(this);
    }

    private void Update() => ProcessInput();

    /// <summary>
    /// Gets the user input and acts accordingly
    /// </summary>
    private void ProcessInput()
    {
        AddVelocity(Input.GetAxis(settings.Input.movementAxis));

        if (Input.GetKeyDown(settings.Input.ShootButton))
            bulletSpawner.Shoot();
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            ProgressLoader.Save(new ProgressData(1, 100));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log(ProgressLoader.Load().LevelScores[1]);
        }
        */
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
        if (Health <= 0)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
