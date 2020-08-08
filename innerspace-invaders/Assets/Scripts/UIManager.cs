using System.Collections;
using System.Collections.Generic;
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
    private Button stopButton;
    [SerializeField]
    private Button quitButton;

    private void Start()
    {
        stopButton.onClick.AddListener(StopGame);
        quitButton.onClick.AddListener(Application.Quit);
        foreach (Button button in playButtons)
            button.onClick.AddListener(PlayGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(settings.Input.PauseMenuButton))
            SwitchState();
    }

    private void StopGame() => stateMachine.SetState(GameState.Stopped);
    private void PlayGame() => stateMachine.SetState(GameState.Playing);

    private void SwitchState()
    {
        if (stateMachine.CurrentState == GameState.Playing)
            stateMachine.SetState(GameState.Paused);
        else if (stateMachine.CurrentState != GameState.Playing)
            stateMachine.SetState(GameState.Playing);
    }
}
