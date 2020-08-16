using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A manager for all the audio in the game, please note that i imported snippets from a personal project for this script
/// </summary>
public class AudioManager : StateBehaviour
{
    #region Variables
    #region Singleton instance
    public static AudioManager Instance { get; private set; }
    #endregion
    #region Editor
    [Header("SFX settings")]
    [SerializeField]
    [Range(0, 1)]
    private float globalEffectsVolume;
    /// <summary>
    /// The audioChannels used to fade from track to track.
    /// </summary>
    [SerializeField]
    private AudioSource effectSource;
    /// <summary>
    /// All of the sound effects the application uses.
    /// </summary>
    [Tooltip("All of the sound effects the application uses.")]
    [SerializeField]
    private List<SoundClip> soundClips;

    [Header("Music settings")]
    [SerializeField]
    [Range(0, 1)]
    private float globalMusicVolume;
    [SerializeField]
    private bool playMusicOnStart;
    /// <summary>
    /// The time to fade from one background track to another.
    /// </summary>
    [Tooltip("The time to fade from one background track to another.")]
    [SerializeField]
    private float musicFadeDuration;
    /// <summary>
    /// The AudioSource for SFX
    /// </summary>
    [SerializeField]
    private AudioSource[] musicChannels;
    /// <summary>
    /// All of the sound effects the application uses.
    /// </summary>
    [Tooltip("All of the sound effects the application uses.")]
    [SerializeField]
    private List<SoundClip> musicClips;
    private int activeTrackIndex = -1;
    #endregion
    #endregion

    #region Methods
    #region Unity
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        if (playMusicOnStart)
            PlayBackgroundTrack(0);        
    }
    #endregion
    #region Public
    /// <summary>
    /// Plays an audio clip
    /// </summary>
    /// <param name="clipIndex">The clip index of the clip you wish to play.</param>
    public void PlayClip(int clipIndex)
    {
        effectSource.pitch = soundClips[clipIndex].Pitch;
        effectSource.PlayOneShot(soundClips[clipIndex].sound, soundClips[clipIndex].Volume * globalEffectsVolume);
    }
    /// <summary>
    /// Plays a background track by the clip index
    /// </summary>
    /// <param name="clipIndex"></param>
    public void PlayBackgroundTrack(int clipIndex)
    {
        if (activeTrackIndex == clipIndex)
            return;
        activeTrackIndex = clipIndex;

        // pick the empty audiochannel and load the chosen track
        int chosenChannel = musicChannels[0].clip == null ? 0 : 1;
        if (musicChannels[chosenChannel].clip != musicClips[clipIndex].sound)
            musicChannels[chosenChannel].clip = musicClips[clipIndex].sound;
        //musicChannels[chosenChannel].volume = 0;

        if (!musicChannels[chosenChannel].isPlaying)        
            musicChannels[chosenChannel].Play();
        StartCoroutine(FadeTracks(chosenChannel));
    }
    #endregion
    #region Coroutines
    /// <summary>
    /// Fades the music to the given channel index
    /// </summary>
    /// <param name="channelIndex"></param>
    /// <returns></returns>
    private IEnumerator FadeTracks(int channelIndex)
    {
        // Set a timer
        float timer = 0;
        // Check which channel will be faded out
        int otherChannel = channelIndex == 1 ? 0 : 1;
        // Fade in the new channel and fade out the old one simultaniously
        while (timer <= musicFadeDuration)
        {
            timer += Time.deltaTime;
            musicChannels[channelIndex].volume = timer / musicFadeDuration * (musicClips[channelIndex].Volume * globalMusicVolume);
            musicChannels[otherChannel].volume = (1 - timer / musicFadeDuration) * (musicClips[otherChannel].Volume * globalMusicVolume);
            yield return null;
        }
        if (timer >= musicFadeDuration)
        {
            // remove the clip from the other channel            
            musicChannels[otherChannel].clip = null;
        }
        yield return null;
    }
    #endregion

    public override void UpdateState(GameState state, GameState oldState)
    {
        if (state == GameState.Stopped)
            PlayBackgroundTrack(0);
        if (state == GameState.Playing)
            PlayBackgroundTrack(1);
    }

    #endregion
}
