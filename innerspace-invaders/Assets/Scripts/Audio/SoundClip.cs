using System;
using UnityEngine;

[Serializable]
public struct SoundClip
{
    public AudioClip sound;
    /// <summary>
    /// The volume of the clip.
    /// </summary>
    [Range(0f, 1f)] public float Volume;
    /// <summary>
    /// The pitch that the clip will be played with
    /// </summary>
    [Range(-3f, 3f)] public float Pitch;
}
