using UnityEngine;

public abstract class Bullet : MonoBehaviour, IBullet
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxLifeTime;

    private float currentLifeTime;

    private void OnEnable() => currentLifeTime = 0;

    private void Update() => Move();

    public void Move()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed, Space.Self);
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > maxLifeTime)
            Explode();        
    }

    public void Initialize(Transform setTransform, BulletType type)
    {
        transform.position = setTransform.position;
        transform.rotation = setTransform.rotation;
        BulletType = type;
    }

    public GameObject Object => gameObject;

    public BulletType BulletType { get; private set; }

    public virtual void Explode()
    {
        Debug.Log("explode");
    }
}
