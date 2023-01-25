using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Class of leaderboard saves and shows best race time.
/// </summary>
public class LeaderBoard : MonoBehaviour
{
    [Tooltip("Timer variable.")]
    [SerializeField] private Timer timer;

    [Tooltip("How much position should be display.")]
    public int numberOfPosition;
    private static int _numberOfPistion;

    [Tooltip("Text objects from UI which will display best time. That varibale should be equal to Number Of Position.")]
    public TextMeshProUGUI[] highTimeScoreText;

    private string[] formatedTime;
    private List<float> savedTimes;



    private void OnEnable()
    {
        Events.OnStopRace += CheckRaceTime;
    }

    private void OnDisable()
    {
        Events.OnStopRace -= CheckRaceTime;
    }

    private void Awake()
    {
        // Some trick to be able to change length of savedTimes and fromatedTime from Inspector.
        _numberOfPistion = numberOfPosition;

        // Initialize new string array with number of position.
        formatedTime = new string[_numberOfPistion];

        // Initialize new List with number of position.
        savedTimes = new List<float>(new float[_numberOfPistion]);

    }

    private void Start()
    {
        CheckIfPrefsSet();
        GetBestTime();
    }

    /// <summary>
    /// Get the Best Times from PlayerPrefs and put it in savedTimes to display on screen.
    /// </summary>
    private void GetBestTime()
    {

        for (int i = 0; i <= _numberOfPistion - 1; i++)
        {
            if (PlayerPrefs.HasKey("fastTime" + (i+1).ToString()))
            {
                savedTimes[i] = PlayerPrefs.GetFloat("fastTime" + (i+1).ToString());
            }
        }

        FormatTimesToString();
    }

    /// <summary>
    /// Set the Best Times to PlayerPrefs.
    /// </summary>
    private void SetBestTime()
    {
            for (int i = 0; i <= _numberOfPistion - 1; i++)
            {
                PlayerPrefs.SetFloat("fastTime" + (i + 1).ToString(), savedTimes[i]);
            }

        FormatTimesToString();
    }

    /// <summary>
    /// Check race time and set it to savedTimes if it best.
    /// </summary>
    private void CheckRaceTime()
    {
        int scorePosition = int.MaxValue;
        bool highScore = false;

        // loop backwards through the savedTimes and check if we have beaten any times
        for(int i = _numberOfPistion - 1; i >= 0; i--)
        {
            // Check times and also check if time slot is unsaved/0
            if(timer.currentTime < savedTimes[i] || savedTimes[i] == 0)
            {
                highScore = true;

                // If i is less than oru current position make that our current position
                if(i < scorePosition)
                    scorePosition = i;
            }
        }

        // If we have a high score, insert it into our times list and then Set the New Best Times player prefs
        if (highScore)
        {
            savedTimes.Insert(scorePosition, timer.currentTime);
            SetBestTime();

            highTimeScoreText[scorePosition].color = Color.red;
        }
    }

    /// <summary>
    /// Format Timer value from float to string.
    /// </summary>
    private void FormatTimesToString()
    {
        for(int i = _numberOfPistion-1; i>=0; i--)
        {
            TimeSpan t = TimeSpan.FromSeconds(savedTimes[i]);
            formatedTime[i] = t.ToString("m':'ss':'ff");

            //  Display timer value on the screen.
            highTimeScoreText[i].text = t.ToString("m':'ss':'ff");
        }
    }

    /// <summary>
    /// Check for the presense of saved time in PlayerPrefs.
    /// </summary>
    private void CheckIfPrefsSet()
    {
        for (int i = 0; i <= _numberOfPistion; i++)
        {
            // If we don't have our PlayerPref's set then up with a deafault value of 0.
            if (!PlayerPrefs.HasKey("fastTime" + i.ToString()))
            {
                PlayerPrefs.SetFloat("fastTime" + i.ToString(), 0);
            }
        }
    }
}
