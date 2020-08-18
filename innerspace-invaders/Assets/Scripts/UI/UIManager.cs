using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameSettings settings;
    [SerializeField]
    private GameStateMachine stateMachine;
    [SerializeField]
    private Button[] playButtons;
    [SerializeField]
    private Button[] stopButtons;
    [SerializeField]
    private Button quitButton;

    private void Start()
    {
        foreach (Button button in stopButtons)
            button.onClick.AddListener(StopGame);
        quitButton.onClick.AddListener(Application.Quit);
        foreach (Button button in playButtons)
            button.onClick.AddListener(PlayGame);
        InputManager.Instance.processPause += SwitchState;
    }

    private void StopGame() => stateMachine.SetState(GameState.Stopped);
    private void PlayGame() => stateMachine.SetState(GameState.Playing);

    private void SwitchState()
    {
        if (stateMachine.CurrentState == GameState.Playing)
            stateMachine.SetState(GameState.Paused);
        else if (stateMachine.CurrentState == GameState.Paused)
            stateMachine.SetState(GameState.Playing);
    }
}
