using UnityEngine;

[CreateAssetMenu(fileName = "Input settings", menuName = "Settings/Input")]
public class InputSettings : ScriptableObject
{
    public KeyCode ShootButton;
    public string movementAxis;
}
