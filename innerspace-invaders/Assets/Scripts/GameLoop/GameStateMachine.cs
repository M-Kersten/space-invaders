using UnityEngine;

/// <summary>
/// Central manager for controlling the gameloop
/// </summary>
public class GameStateMachine : MonoBehaviour
{
    [System.Serializable]
    private struct StateBasedObject
    {
        public GameObject stateObject;
        public GameState activeStates;
    }
    /// <summary>
    /// An array of objects that are able to change the game state
    /// </summary>
    [SerializeField]
    private StateBehaviour[] stateChangers;
    /// <summary>
    /// An array of objects that react to state changes
    /// </summary>
    [SerializeField]
    private StateBasedObject[] stateBasedObjects;

    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        foreach (StateBehaviour item in stateChangers)
            item.SetState += SetState;
    }

    private void Start()
    {
        SetState(GameState.Stopped);
    }

    private void OnDestroy()
    {
        foreach (StateBehaviour item in stateChangers)
            item.SetState -= SetState;
    }

    /// <summary>
    /// Set a new state for the game loop
    /// </summary>
    /// <param name="state"></param>
    public void SetState(GameState state)
    {
        foreach (StateBasedObject obj in stateBasedObjects)
            obj.stateObject.SetActive(obj.activeStates.HasFlag(state));
        
        foreach (StateBehaviour item in stateChangers)     
            item.UpdateState(state, CurrentState);

        CurrentState = state;
    }
}
