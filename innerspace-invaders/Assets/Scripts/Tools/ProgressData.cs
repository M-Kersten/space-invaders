using System;
using System.Collections.Generic;

/// <summary>
/// Template for general progress stats for player
/// </summary>
[Serializable]
public class ProgressData
{
    /// <summary>
    /// Dictionary holding all the levels with respective stars rewarded on said level
    /// </summary>
    public Dictionary<int, float> LevelScores = new Dictionary<int, float>();

    public ProgressData(int level, float score)
    {
        LevelScores.Add(level, score);
    }  
}
