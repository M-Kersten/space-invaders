using UnityEngine;

public class GameStateMachine : MonoBehaviour
{    
    [System.Serializable]
    private struct StateBasedObject
    {
        public GameObject stateObject;
        public GameState activeStates;
    }

    [SerializeField]
    private StateBehaviour[] stateChangers;
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

    public void SetState(GameState state)
    {
        foreach (StateBasedObject obj in stateBasedObjects)
            obj.stateObject.SetActive(obj.activeStates.HasFlag(state));
        
        foreach (StateBehaviour item in stateChangers)     
            item.UpdateState(state, CurrentState);

        CurrentState = state;
    }
}
