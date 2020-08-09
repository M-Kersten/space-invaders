using System;

[Flags]
public enum GameState
{
    Stopped = 1,
    Playing = 2,
    Paused = 4,
    Lost = 8,
    NextLevel = 16
}