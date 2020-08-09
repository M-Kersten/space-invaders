using System;
using UnityEngine;

/// <summary>
/// Base class for objects the react to and are able to change the game state
/// </summary>
public abstract class StateBehaviour: MonoBehaviour
{
    internal GameState CurrentState { get; set; }
    public virtual void UpdateState(GameState state, GameState previousState) { }
    public Action<GameState> SetState;
}
