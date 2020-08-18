using UnityEngine;
using UnityEngine.UI;

public class DesktopInput : InputPreset
{
    [SerializeField]
    private GameSettings settings;

    private KeyCode shoot;
    private string movementAxis;
    private KeyCode pause;

    private void Update()
    {
        ProcessInput();
    }

    private void ProcessInput()
    {
        if (CurrentState != GameState.Playing)
            return;

        if (Input.GetKeyDown(pause))
            Pause?.Invoke();
        if (Input.GetKeyDown(shoot))        
            Shoot?.Invoke();
        
        if (Input.GetAxis(movementAxis) != 0)
            Move?.Invoke(Input.GetAxis(movementAxis));        
    }

    public override void Initialize()
    {
        shoot = settings.Input.ShootButton;
        movementAxis = settings.Input.movementAxis;
        pause = settings.Input.PauseMenuButton;
    }

    public override void UpdateState(GameState state, GameState previousState)
    {
        CurrentState = state;
    }
}
