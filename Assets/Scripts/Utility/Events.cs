using UnityEngine;

/// <summary>
/// Class of game events.
/// </summary>
public class Events : MonoBehaviour
{
    // Events when player take damage(hit).
    public delegate void hitPlayerAction();
    public static event hitPlayerAction OnHitPlayer;

    // Race action events.
    public delegate void raceAction();
    public static event raceAction OnStartRace;
    public static event raceAction OnStopRace; 
    public static event raceAction OnUpScore;

    // Menu, panels and button clicks events.
    public delegate void menuAction();
    public static event menuAction OnShowGameOverPanel;
    public static event menuAction OnShowAreYouSurePanel;
    public static event menuAction OnCreditsPanel;
    public static event menuAction OnShowPausePanel;
    public static event menuAction OnShowSettingsPanel;
    public static event menuAction OnShowHighScorePanel;
    public static event menuAction OnShowFirstLevelMapPanel;
    public static event menuAction OnBackButtonActions;
    public static event menuAction OnRetryRace;
    public static event menuAction OnQuitGame;

    // Level loading event.
    public delegate void levelLoadaAction(string level);
    public static event levelLoadaAction OnLoadLevel;

    // Sounds event.
    public delegate void gameSoundsAction();
    public static event gameSoundsAction OnClickButton;




    public static void HitPlayer()
    {
        if (OnHitPlayer != null)
            OnHitPlayer();
    }

    public static void StartRace()
    {
        if(OnStartRace!= null)
            OnStartRace();
    }

    public static void StopRace()
    {
        if (OnStopRace != null)
            OnStopRace();
    }

    public static void IncreaseScore()
    {
        if (OnUpScore != null)
            OnUpScore();
    }

    public void RetryRace()
    {
        if (OnRetryRace != null)
            OnRetryRace();
    }

    public void LoadLevel(string level)
    {
        if (OnLoadLevel != null)
            OnLoadLevel(level);
    }

    public void QuitGame()
    {
        if (OnQuitGame != null)
            OnQuitGame();
    }

    public void BackButtonActions()
    {
        if (OnBackButtonActions != null)
            OnBackButtonActions();
    }

    public void ShowGameOverPanel()
    {
        if (OnShowGameOverPanel != null)
            OnShowGameOverPanel();
    }


    public void ShowAreYouSurePanel()
    {
        if (OnShowAreYouSurePanel != null)
            OnShowAreYouSurePanel();
    }

    public void ShowCreditsPanel()
    {
        if (OnCreditsPanel != null)
            OnCreditsPanel();
    }

    public void ShowPausePanel()
    {
        if (OnShowPausePanel != null)
            OnShowPausePanel();
    }

    public void ShowSettingsPanel()
    {
        if(OnShowSettingsPanel != null)
            OnShowSettingsPanel();
    }

    public void ShowHighScorePanel()
    {
        if(OnShowHighScorePanel != null)
            OnShowHighScorePanel();
    }


    public void ShowFirstLevelMapPanel()
    {
        if(OnShowFirstLevelMapPanel != null)
            OnShowFirstLevelMapPanel();
    }

    public void PlaySoundOnClickButton()
    {
        if (OnClickButton != null)
            OnClickButton();
    }
}
