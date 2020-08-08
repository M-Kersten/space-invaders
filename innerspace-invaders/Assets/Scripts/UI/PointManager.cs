using Invaders.Tools;
using System;
using UnityEngine;

/// <summary>
/// Manages the accumulation and reset of points acquired by the player
/// </summary>
public class PointManager : StateBehaviour
{
    // due to time constraints, decided to implement score traking the easy way with a singleton, please forgive me :P
    #region Singleton instance
    public static PointManager Instance { get; private set; }
    #endregion

    /// <summary>
    /// Whenever a new highscore has been achieved, call this action
    /// </summary>
    public Action<int> HighScoreUpdated;
    /// <summary>
    /// The active scoredisplay for the current level score
    /// </summary>
    [SerializeField]
    private ScoreDisplay[] displays;
    /// <summary>
    /// Current score the player has aquired in this level
    /// </summary>
    private int currentScore
    {
        get { return score; }
        set
        {
            score = value; 
            foreach (ScoreDisplay display in displays)
                display.UpdateDisplay(score); 
        } }
    private int score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogError($"multiple instances of {this} not allowed on gameobject: {gameObject.name}");
            Destroy(this);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(Constants.KEY_HIGHSCORE))
            HighScoreUpdated?.Invoke(PlayerPrefs.GetInt(Constants.KEY_HIGHSCORE));
    }

    public override void UpdateState(GameState state)
    {
        if (state == GameState.Stopped)
        {
            SaveScore();
            ResetScore();
        }
    }

    /// <summary>
    /// Saves the current score as a new highscore when it's higher than the current highscore
    /// </summary>
    public void SaveScore()
    {
        if (!PlayerPrefs.HasKey(Constants.KEY_HIGHSCORE) || PlayerPrefs.GetInt(Constants.KEY_HIGHSCORE) < score)
        {
            PlayerPrefs.SetInt(Constants.KEY_HIGHSCORE, score);
            HighScoreUpdated?.Invoke(PlayerPrefs.GetInt(Constants.KEY_HIGHSCORE));
        }
    }
    /// <summary>
    /// Reset current level score
    /// </summary>
    public void ResetScore() => currentScore = 0;
    /// <summary>
    /// Increments the score
    /// </summary>
    public void AddScore(int scoreAmount) => currentScore += scoreAmount;    
}
