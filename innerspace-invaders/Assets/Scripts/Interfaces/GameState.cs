using System;

[Flags]
public enum GameState
{
    Stopped = 1,
    Playing = 2,
    Paused = 4
}