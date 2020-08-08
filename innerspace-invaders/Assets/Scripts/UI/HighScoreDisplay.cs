using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class HighScoreDisplay : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        UpdateDisplay(PointManager.Instance.HighScore);
        PointManager.Instance.HighScoreUpdated += UpdateDisplay;
    }

    private void OnDisable()
    {
        PointManager.Instance.HighScoreUpdated -= UpdateDisplay;
    }

    public void UpdateDisplay(int score)
    {
        scoreText.text = score.ToString();
    }
}
