using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level collection", menuName = "Settings/Level collection")]
public class LevelCollection : ScriptableObject
{
    public int CurrentLevel;
    public LevelSettings[] Levels;
}
