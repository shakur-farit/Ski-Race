using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Overall Pannels Components")]
    [Space(10)]
    [Tooltip("Overlay UI Image for screen transitions.")]
    [SerializeField] private GameObject overlayScreen;
    [Tooltip("Settings pop up UI panel.")]
    [SerializeField] private GameObject settingsPanel;
    [Tooltip("High Score pop up UI panel.")]
    [SerializeField] private GameObject highScorePanel;

    [Header("Main Menu Pannels Components")]
    [Space(10)]
    [Tooltip("Are You Sure pop up UI panel.")]
    [SerializeField] private GameObject areYouSurePanel;
    [Tooltip("Credits pop up UI panel.")]
    [SerializeField] private GameObject creditsPanel;
    [Tooltip("Main Menu UI panel.")]
    [SerializeField] private GameObject mainMenu;

    [Header("Level Choose Menu Pannels Components")]
    [Space(10)]
    [Tooltip("Second level map panel")]
    [SerializeField] private GameObject secondLevelMapPanel;
    [Tooltip("Level map button's locked state")]
    [SerializeField] private GameObject secondLevelMapLocked;
    [Tooltip("Level map button's unlocked state")]
    [SerializeField] private GameObject secondLevelMapUnlocked;

    [Header("Race Pannels Components")]
    [Space(10)]
    [Tooltip("Race Over pop up UI panel.")]
    [SerializeField] private GameObject raceOverPanel;
    [Tooltip("Game Over pop up UI panel.")]
    [SerializeField] private GameObject gameOverPanel;
    [Tooltip("Pause pop up UI panel.")]
    [SerializeField] private GameObject pausePanel;

    private static GameManager instance;

    private GameObject pauseButton;


    public static GameManager Instance { get; private set; }

    private void OnEnable()
    {
        Events.OnStopRace += ShowRaceOverPanel;
        Events.OnRetryRace += RestartRace;
        Events.OnLoadLevel += LoadLevel;
        Events.OnQuitGame += QuitGame;

        Events.OnShowGameOverPanel += ShowGameOverPanel;

        Events.OnBackButtonActions += BackButton;

        Events.OnShowAreYouSurePanel += ShowAreYouSurePanel;
        Events.OnCreditsPanel += ShowCreditsPanel;
        Events.OnShowPausePanel += ShowPausePanel;
        Events.OnShowSettingsPanel += ShowSettingsPanel;
        Events.OnShowHighScorePanel += ShowHighScorePanel;

    }

    private void OnDisable()
    {
        Events.OnStopRace -= ShowRaceOverPanel;
        Events.OnRetryRace -= RestartRace;
        Events.OnLoadLevel -= LoadLevel;
        Events.OnQuitGame -= QuitGame;

        Events.OnShowGameOverPanel -= ShowGameOverPanel;

        Events.OnBackButtonActions -= BackButton;

        Events.OnShowAreYouSurePanel -= ShowAreYouSurePanel;
        Events.OnCreditsPanel -= ShowCreditsPanel;
        Events.OnShowPausePanel -= ShowPausePanel;
        Events.OnShowSettingsPanel -= ShowSettingsPanel;
        Events.OnShowHighScorePanel -= ShowHighScorePanel;

    }

    private void Awake()
    {
        if (instance == null)
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
        // At the start of our scene we will fade out the Overlay.
        overlayScreen.GetComponent<Image>().CrossFadeAlpha(0, 1, false);
        overlayScreen.GetComponent<Image>().raycastTarget = false;

        // Find game object with PauseButton name in hierarchy and initialize pauseButton.
        pauseButton = GameObject.Find("PauseButton");

        if (SceneManager.GetActiveScene().name == "LevelChooseMenu")
        {
            if (gameObject.GetComponent<SingleLevel>().lastLevelIndexInFirstMap <= PlayerPrefs.GetInt("Level"))
            {
                Debug.Log(gameObject.GetComponent<SingleLevel>().lastLevelIndexInFirstMap);
                ShowSecondLevelMapButton();
            }
        }

    }

    /// <summary>
    /// Show Race Over Panel when collide with fonish line.
    /// </summary>
    private void ShowRaceOverPanel()
    {
        raceOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Show Game Over Panel when Player is die (currentHealt = 0)
    /// </summary>
    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Show Are You Sure? panel in Main Menu scene.
    /// </summary>
    private void ShowAreYouSurePanel()
    {
        areYouSurePanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Show Credits Panel. 
    /// </summary>
    private void ShowCreditsPanel()
    {
        creditsPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Show Pause Panel when paused game.
    /// </summary>
    private void ShowPausePanel()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;

        pauseButton.SetActive(false);
    }

    /// <summary>
    /// Show Settings Panel in game or Main Menu scene.
    /// </summary>
    private void ShowSettingsPanel()
    {
        // If NOT in Main Menu scene (first in Build Settings (0)) 
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            settingsPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
            mainMenu.SetActive(false);
        }
    }

    /// <summary>
    /// Show Settings Panel in game or Main Menu scene.
    /// </summary>
    private void ShowHighScorePanel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            highScorePanel.SetActive(true);
            pausePanel.SetActive(false);
        }
        else
        {
            highScorePanel.SetActive(true);
            mainMenu.SetActive(false);
        }
    }


    /// <summary>
    /// When complete last level in first map unlocked second map.
    /// </summary>
    private void ShowSecondLevelMapButton()
    {
        secondLevelMapLocked.SetActive(false);
        secondLevelMapUnlocked.SetActive(true);
    }

    /// <summary>
    /// Restarted race.
    /// </summary>
    private void RestartRace()
    {
        // Reload the game scene after pressing the Retry button.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Load other level or start with Level 1 if in Main Menu scene.
    /// </summary>
    private void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Quit from application if in Main Menu scene or from game to Main Menu scene. 
    /// </summary>
    private void QuitGame()
    {
        // If NOT in Main Menu scene (first in Build Settings (0)) load Main Menu (0) scene.
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex * 0);
        }
        else // Quit from aplication.
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    /// <summary>
    /// Back Button's actions. Also on "Resume" button for unpaused.
    /// </summary>
    private void BackButton()
    {
        // If in Main Menu scene use BackToMainMenuButton for Back button.
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            BackToMainMenuButton();
        }
        else
        {
            // If Settings Panel is open.
            if (settingsPanel.activeInHierarchy == true)
            {
                CloseSettingsPanel();
            }
            // If High Score Panel is open.
            else if (highScorePanel.activeInHierarchy == true)
            {
                CloseHighScorePanel();
            }
            else
            {
                ClosePausePanel();
            }
        }
    }

    /// <summary>
    /// Back to main menu in Main Menu scene.
    /// </summary>
    private void BackToMainMenuButton()
    {
        // If Settings Panel is open.
        if (settingsPanel.activeInHierarchy == true)
        {
            CloseSettingsPanel();
        }

        // If Credits Panel is open.
        if (creditsPanel.activeInHierarchy == true)
        {
            CloseCreditsPanel();
        }

        // If Are You Sure Panel is open.
        if (areYouSurePanel.activeInHierarchy == true)
        {
            CloseAreYouSurePanel();
        }

        // If High Score Panel is open.
        if (highScorePanel.activeInHierarchy == true)
        {
            CloseHighScorePanel();
        }

    }

    /// <summary>
    /// Unpaused game.
    /// </summary>
    private void ClosePausePanel()
    {

        pausePanel.SetActive(false);
        Time.timeScale = 1;

        pauseButton.SetActive(true);
    }

    /// <summary>
    /// Close Settings Panel and comeback to Pause Panel.
    /// </summary>
    private void CloseSettingsPanel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(false);
            mainMenu.SetActive(true);
        }

        Debug.Log("Settings");
    }

    /// <summary>
    /// Close High Score Panel in game or Main Menu scene.
    /// </summary>
    private void CloseHighScorePanel()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            highScorePanel.SetActive(false);
            pausePanel.SetActive(true);
        }
        else
        {
            highScorePanel.SetActive(false);
            mainMenu.SetActive(true);
        }

        Debug.Log("Highs");
    }

    /// <summary>
    /// Close Credits Panel.
    /// </summary>
    private void CloseCreditsPanel()
    {
        creditsPanel.SetActive(false);
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// Close Are You Sure Panel.
    /// </summary>
    private void CloseAreYouSurePanel()
    {
        areYouSurePanel.SetActive(false);
        mainMenu.SetActive(true);
    }
}
