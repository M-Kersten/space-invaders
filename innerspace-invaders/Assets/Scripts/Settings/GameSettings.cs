using UnityEngine;

[CreateAssetMenu(fileName = "Game settings", menuName = "Settings/Game")]
public class GameSettings : ScriptableObject
{
    [Header("Input")]
    public InputSettings Input;
    [Header("Player")]
    public float PlayerSpeed;
    public float PlayerShotCooldown;
    public int InitialPlayerHealth;
    public Vector2Int PlayerBounds;
}
