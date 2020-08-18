using UnityEngine;
using UnityEngine.UI;

public class PhoneControls : MonoBehaviour
{
    void Start()
    {
        // if not running on a mobile device, disable the control panel
#if !UNITY_IOS && !UNITY_ANDROID
        gameObject.SetActive(false);
        return;
#endif
    }    
}
