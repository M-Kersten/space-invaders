using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake() => scoreText = GetComponent<TextMeshProUGUI>();

    public void UpdateDisplay(int score)
    {
        if (scoreText == null)
            return;
        scoreText.text = score.ToString();
    }
}
