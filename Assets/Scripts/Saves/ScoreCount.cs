using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour
{
    [Header("Score Components")]
    [Space(10)]
    [Tooltip("Score text.")]
    [SerializeField] private TMP_Text scoreText;
    [Tooltip("High score text.")]
    [SerializeField] private TMP_Text highScoreText;

    private int currentNumberOfScore;

    private void OnEnable()
    {
        Events.OnUpScore += ChangeNumberOFScore;
    }

    private void OnDisable()
    {
        Events.OnUpScore -= ChangeNumberOFScore;
    }

    private void Start()
    {
        // Start with scene with 0 current score.
        scoreText.text = "0";

        // If we have highest score start with it.
        highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void ChangeNumberOFScore()
    {
        currentNumberOfScore += 10;
        scoreText.text = currentNumberOfScore.ToString();

        if (currentNumberOfScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", currentNumberOfScore);
            highScoreText.text = currentNumberOfScore.ToString();
        }
    }

}
