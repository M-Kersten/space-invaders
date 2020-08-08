using UnityEngine;

public class NewHighScoreDisplay : StateBehaviour
{
    private bool newSession;

    private void OnEnable()
    {
        PointManager.Instance.HighScoreUpdated += UpdateDisplay;
        transform.localScale = Vector3.zero;
    }

    private void OnDisable()
    {
        PointManager.Instance.HighScoreUpdated -= UpdateDisplay;
    }

    public void UpdateDisplay(int score)
    {
        if (!newSession)
            return;
        newSession = false;
        LeanTween.scale(gameObject, Vector3.one, 1).setEase(LeanTweenType.easeInOutQuart).setOnComplete(() => LeanTween.scale(gameObject, Vector3.zero, 1).setEase(LeanTweenType.easeInOutQuart).setDelay(3));
    }

    public override void UpdateState(GameState state, GameState previousState) 
    {
        if (state == GameState.Playing && (previousState == GameState.Lost || previousState == GameState.Stopped))
        {
            newSession = true;
        }
    }
}

