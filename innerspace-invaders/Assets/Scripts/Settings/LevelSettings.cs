using UnityEngine;

[CreateAssetMenu(fileName = "Level settings", menuName = "Settings/Level")]
public class LevelSettings : ScriptableObject
{
    [Header("Difficulty")]
    public int GridRows;
    public int GridCollumns;
    public float EnemySpeed;
    [Header("Visual")]
    public Color[] EnemyColors;    
    public Color BackgroundColor;
}
