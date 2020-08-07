using UnityEngine;

/// <summary>
/// Collection of constant values that should be accessible in the whole project
/// </summary>
namespace Invaders.Tools
{
    /// <summary>
    /// Collection of constant values that should be accessible in the whole project, collected in 1 location to make looking up values quicker
    /// </summary>
    public class Constants
    {
        #region Playerprefs
        /// <summary>
        /// Max level the player has reached, used to lock subsequent levels
        /// </summary>
        public const string STARSCOLLECTED = "StarsCollected";
        public const string MUTED = "MuteAudio";
        #endregion
        #region Tags
        public const string BULLET = "Bullet";
        #endregion
        #region Saving
        /// <summary>
        /// Since application.datapath is a runtime variable, this string is static readonly
        /// </summary>
        public static readonly string SAVEFOLDER = Application.persistentDataPath + "/ProgressData/";
        #endregion
    }
}