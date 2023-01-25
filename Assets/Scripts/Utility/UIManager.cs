using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("Score Components")]
    [Space(10)]
    [Tooltip("Score text.")]
    [SerializeField] private TMP_Text scoreText;
    [Tooltip("High score text.")]
    [SerializeField] private TMP_Text highScoreText;

    [Header("Health UI Components")]
    [Space(10)]
    [Tooltip("Health copmonent.")]
    [SerializeField] private Health health;
    [Tooltip("Sprite of full health image.")]
    [SerializeField] private Sprite fullHealth;
    [Tooltip("Sprite of empty health image.")]
    [SerializeField] private Sprite emptyHealth;
    [Tooltip("Image of health")]
    [SerializeField] private Image[] healthImage;

    private static UIManager instance;
    private int currentNumberOfScore;

    public bool deleteSaves = false;


    public static UIManager Instance { get; private set; }


    private void OnEnable()
    {
        Events.OnUpScore += ChangeNumberOFScore;
    }

    private void OnDisable()
    {
        Events.OnUpScore -= ChangeNumberOFScore;
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



    private void Start()
    {
        // If we not in LevelChooseMenu or LevelMap Scene.
        if (SceneManager.GetActiveScene().name != "LevelChooseMenu" && SceneManager.GetActiveScene().name != "FirstLevelMap"
            && SceneManager.GetActiveScene().name != "SecondLevelMap")
        {
            // If we have highest score start with it.
            highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }

        // For test and checking saves
        if (deleteSaves && SceneManager.GetActiveScene().name == "LevelChooseMenu")
        {
            PlayerPrefs.DeleteAll();
        }

    }



    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex > 3)
        {
            ChangeNumberOfHealthUI();
            ReplaceFullHealthToEmpty();
        }
    }

    /// <summary>
    /// Increase the score.
    /// </summary>
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

    /// <summary>
    /// Set number of Image of health equal Health of the Player
    /// </summary>
    private void ChangeNumberOfHealthUI()
    {
        for (int i = 0; i < healthImage.Length; i++)
        {
            if (i < health.MaximumHealth)
            {
                healthImage[i].enabled = true;
            }
            else
            {
                healthImage[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// Replace Full Health sprite to Empty Health sprite
    /// </summary>
    private void ReplaceFullHealthToEmpty()
    {
        for (int i = 0; i < healthImage.Length; i++)
        {
            if (i < health.CurrentHealth)
            {
                healthImage[i].sprite = fullHealth;
            }
            else
            {
                healthImage[i].sprite = emptyHealth;
            }
        }
    }
}
