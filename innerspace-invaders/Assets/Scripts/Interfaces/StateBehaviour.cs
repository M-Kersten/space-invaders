using System;
using UnityEngine;

public abstract class StateBehaviour: MonoBehaviour
{
    internal GameState CurrentState { get; set; }
    public virtual void UpdateState(GameState state) { }

    public Action<GameState> SetState;
}
