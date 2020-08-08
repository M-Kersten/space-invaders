using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenu : MonoBehaviour
{
    #region Variables
    #region Editor
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private Button stopButton;
    [SerializeField]
    private Button resetButton;
    [SerializeField]
    private Button continueButton;
    #endregion
    #region Private
    private CanvasGroup menuCanvas;
    #endregion
    #endregion

    #region Methods
    #region Unity
    private void Awake() => menuCanvas = GetComponent<CanvasGroup>();

    private void Start()
    {
        // connect all buttons in the pause menu to the logic
        pauseButton.onClick.AddListener(OpenPauseMenu);
        stopButton.onClick.AddListener(StopGame);
        resetButton.onClick.AddListener(ResetGame);
       // continueButton.onClick.AddListener(ContinueGame);
    }
    #endregion
    #region Public
    /// <summary>
    /// stops and plays the game
    /// </summary>
    public void ResetGame()
    {
        //GameStateManager.State = GameState.Stopped;
       // ClosePauseMenu(() => GameStateManager.State = GameState.Playing);
    }
    /// <summary>
    /// Unpauses the game
    /// </summary>
    //public void ContinueGame() => ClosePauseMenu(() => GameStateManager.State = GameState.Playing);
    /// <summary>
    /// Stops the game and returns to intro scene
    /// </summary>
    public void StopGame()
    {
      //  GameStateManager.State = GameState.Stopped;
       // ClosePauseMenu(CanvasAnimator.Instance.ReturnToIntro);
    }
    #endregion
    #region Private
    /// <summary>
    /// Opens the pause menu
    /// </summary>
    private void OpenPauseMenu()
    {
        menuCanvas.blocksRaycasts = true;
        menuCanvas.interactable = true;
        LeanTween.alphaCanvas(menuCanvas, 1, .5f);
        //GameStateManager.State = GameState.Paused;
    }
    /// <summary>
    /// Closes the pause menu
    /// </summary>
    private void ClosePauseMenu(Action callback = null)
    {
        menuCanvas.blocksRaycasts = false;
        menuCanvas.interactable = false;
        LeanTween.alphaCanvas(menuCanvas, 0, .5f)
            .setOnComplete(() => callback?.Invoke());
    }
    #endregion
    #endregion
}
