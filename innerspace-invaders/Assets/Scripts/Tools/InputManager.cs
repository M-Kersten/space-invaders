using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region Singleton instance
    public static InputManager Instance { get; private set; }
    #endregion
    [SerializeField]
    private InputPreset[] presets;

    public Action processShoot;
    public Action<float> processMove;
    public Action processPause;

    private void Awake() => Instance = this;

    private void Start()
    {
        foreach (InputPreset preset in presets)
        {
            if (preset.InputActive)
            {
                preset.Shoot += () => processShoot.Invoke();
                preset.Move += MovePlayer;
                preset.Pause += () => processPause.Invoke();
            }
        }
    }
    
    private void MovePlayer(float direction)
    {
        processMove(direction);
    }
}
