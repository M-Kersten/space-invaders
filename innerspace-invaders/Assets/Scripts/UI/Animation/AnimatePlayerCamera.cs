using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerCamera : StateBehaviour
{
    [SerializeField]
    private float cameraAngle;

    private float direction;

    private void Start()
    {
        InputManager.Instance.processMove += UpdateDirection;
    }

    private void UpdateDirection(float setDirection)
    {
        direction = setDirection;
    }    

    private void Update()
    {
        float angle = Mathf.LerpAngle(transform.eulerAngles.z, direction * cameraAngle, Time.deltaTime);
        angle = Mathf.MoveTowardsAngle(angle, 0, Time.deltaTime);
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }


    public override void UpdateState(GameState state, GameState oldState)
    {        
        CurrentState = state;
    }
}
