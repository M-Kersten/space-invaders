using System;
using System.Linq;
using UnityEngine;

public abstract class InputPreset : StateBehaviour
{
    [SerializeField]
    private RuntimePlatform[] platforms;
    public bool InputActive => platforms.Contains(Application.platform);

    public Action Shoot;
    public Action Pause;
    public Action<float> Move;

    private void Start() => Initialize();

    public abstract void Initialize();
}
