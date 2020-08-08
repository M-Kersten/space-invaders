using UnityEngine;

[CreateAssetMenu(fileName = "Level settings", menuName = "Settings/Level")]
public class LevelSettings : ScriptableObject
{
    [Header("Difficulty")]
    public int GridRows;
    public int GridCollumns;
    public AnimationCurve EnemySpeed;
    public int InitialEnemyHealth;
    [Header("Visual")]
    public Color[] EnemyColors;
}
