using System.IO;
using Newtonsoft.Json;
using UnityEditor;

namespace Invaders.Tools
{
    /// <summary>
    /// Class for saving and loading the progress of a player between playsessions
    /// </summary>
    /// <remarks> While Playerprefs is useful, it's not supposed to save more complicated datastructures, in this usecase JSON is an acceptable save format</remarks>
    public static class ProgressLoader
    {
        /// <summary>
        /// Location of directory in datapath to save to
        /// </summary>
        private static string saveLocation = "/ProgressData.txt";

        /// <summary>
        /// Saves the progress a player has made
        /// </summary>
        public static void Save(ProgressData data)
        {
            if (!Directory.Exists(Constants.SAVEFOLDER))
                Directory.CreateDirectory(Constants.SAVEFOLDER);
                        
            string jsonFormattedData = JsonConvert.SerializeObject(data);
            File.WriteAllText(Constants.SAVEFOLDER + saveLocation, jsonFormattedData);
        }

        /// <summary>
        /// Returns the most recent playerprogress
        /// </summary>
        public static ProgressData Load()
        {
            // if no directory exists, we didn't save anything yet
            if (!Directory.Exists(Constants.SAVEFOLDER) || !File.Exists(Constants.SAVEFOLDER + saveLocation))
                return null;

            string jsonData = File.ReadAllText(Constants.SAVEFOLDER + saveLocation);
            // Using json convert because it supports deserializing dictionaries (JsonUtility doesn't AFAIK)
            ProgressData returnData = JsonConvert.DeserializeObject<ProgressData>(jsonData);
            return returnData;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Clearing of progress for debugging purposes
        /// </summary>
        [MenuItem("Tools/Clear Player levelprogress")]
        public static void Clear()
        {
            File.Delete(Constants.SAVEFOLDER + saveLocation);
        }
#endif
    }
}