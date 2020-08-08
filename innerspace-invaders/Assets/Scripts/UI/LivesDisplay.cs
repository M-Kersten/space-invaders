using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class LivesDisplay : MonoBehaviour
{
    [SerializeField]
    private GameObject livesObject;

    private TextMeshProUGUI livesAmountText;

    private void Start()
    {
        livesAmountText = GetComponent<TextMeshProUGUI>();
        IDamagable damagable = livesObject.GetComponent<IDamagable>();
        if (damagable != null)
            damagable.HealtChanged += UpdateDisplay;
    }

    private void UpdateDisplay(int lives)
    {
        livesAmountText.text = lives.ToString();
    }
}
