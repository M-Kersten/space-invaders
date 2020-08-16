using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayerCamera : StateBehaviour
{
    [SerializeField]
    private GameSettings settings;
    [SerializeField]
    private float cameraAngle;

    private float direction;

    private void Update()
    {
        ProcessInput();

        float angle = Mathf.LerpAngle(transform.eulerAngles.z, direction, Time.deltaTime);
        angle = Mathf.MoveTowardsAngle(angle, 0, Time.deltaTime);
        
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
    }

    private void ProcessInput()
    {
        if (CurrentState != GameState.Playing)
            return;

        direction = Input.GetAxis(settings.Input.movementAxis) * cameraAngle;
    }

    public override void UpdateState(GameState state, GameState oldState)
    {        
        CurrentState = state;
    }
}
