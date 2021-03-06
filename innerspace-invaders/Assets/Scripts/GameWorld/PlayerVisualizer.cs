﻿using UnityEngine;

public class PlayerVisualizer : MonoBehaviour
{
    [SerializeField] 
    private Player player;
    [SerializeField] 
    private Transform carTransform;
    [SerializeField]
    private float smoothingDelay;

    [SerializeField] private float steerAngle;
    
    private float direction;
    
    private void OnEnable()
    {
        player.DirectionChanged += RotateCar;
    }

    private void OnDisable()
    {
        player.DirectionChanged -= RotateCar;
    }

    private void Update()
    {
        float angle = Mathf.MoveTowardsAngle(direction, 0, Time.deltaTime * (1/smoothingDelay));
        carTransform.eulerAngles = new Vector3(0,angle,0);
    }

    private void RotateCar(float steerDirection)
    {
        direction = steerDirection * steerAngle;
    }
}
