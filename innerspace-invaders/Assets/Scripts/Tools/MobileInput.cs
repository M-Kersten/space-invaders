using UnityEngine;
using UnityEngine.UI;

public class MobileInput : InputPreset
{
    [SerializeField]
    private Button shootButton;
    [SerializeField]
    private PressingButton leftButton;
    [SerializeField]
    private PressingButton rightButton;
    [SerializeField]
    private Button pauseButton;

    public override void Initialize()
    {
        pauseButton.onClick.AddListener(() => 
            {
                Pause?.Invoke();
                VibrationManager.VibrateSelect();
            }
            );
        shootButton.onClick.AddListener(() =>
        {
            Shoot?.Invoke();
            VibrationManager.VibrateSelect();
        });
    }

    private void Update()
    {
        if (leftButton.IsBeingPressed)
            Move?.Invoke(-1f);
        else if (rightButton.IsBeingPressed)
            Move?.Invoke(1f);
        else
            Move?.Invoke(0);        
    }

    public override void UpdateState(GameState state, GameState previousState)
    {
        CurrentState = state;
    }
}
