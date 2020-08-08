using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletView : MonoBehaviour
{
    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private TrailRenderer trail;
       

    private void OnEnable()
    {
        bullet.Exploded += VisualizeExplosion;
    }

    private void OnDisable()
    {
        bullet.Exploded -= VisualizeExplosion;
    }

    private void VisualizeExplosion()
    {
        trail.Clear();
    }
}
