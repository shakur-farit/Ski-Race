using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class of game sounds managment. 
/// </summary>
public class SoundsManager : MonoBehaviour
{
    [Header("Sounds On/Off Buttons Components.")]
    [Space(10)]
    [Tooltip("Game sounds off button.")]
    [SerializeField] private GameObject gameSoundOffButton;
    [Tooltip("Game sounds on button.")]
    [SerializeField] private GameObject gameSoundOnButton;
    [Tooltip("Background sounds off button.")]
    [SerializeField] private GameObject backgroundSoundOffButton;
    [Tooltip("Background sounds on button.")]
    [SerializeField] private GameObject backgroundSoundOnButton;

    [Header("Audio Source Components.")]
    [Space(10)]
    [Tooltip("Audio source component of Game Sounds object.")]
    [SerializeField] private AudioSource gameSounds;
    [Tooltip("Audio source component of Background Sounds object.")]
    [SerializeField] private AudioSource backgroundSound;


    private void Awake()
    {
        backgroundSound = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        gameSounds = GameObject.FindGameObjectWithTag("GameSounds").GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene().name != "LevelChooseMenu" && SceneManager.GetActiveScene().name != "FirstLevelMap"
            && SceneManager.GetActiveScene().name != "SecondLevelMap")
        {
            if (!GameData.Instance.gameSoundIsMuted)
            {
                // Activated Game Sounds.
                GameSoundsOn();
            }

            // If we muted Game Sounds in erly scene and dont back in MainMenu stay it muted in current scene.
            if (GameData.Instance.gameSoundIsMuted)
            {
                GameSoundsOff();
            }

            // If we muted Background Sounds in erly scene stay it muted in current scene.
            if (GameData.Instance.backgroundSoundIsMuted || SceneManager.GetActiveScene().name != "MainMenu")
            {
                BackgroundSoundsOff();
            }

            // Start background sound from begining when comeback to MainMenu from game levels or in first starting.
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                backgroundSound.Stop();
                backgroundSound.Play();
                BackgroundSoundsOn();
            } 
        }  
    }

    /// <summary>
    /// Deactivate Game Sounds.
    /// </summary>
    public void GameSoundsOff()
    {

        gameSounds.enabled = false;

        gameSoundOffButton.SetActive(false);
        gameSoundOnButton.SetActive(true);

        GameData.Instance.gameSoundIsMuted = true;
    }

    /// <summary>
    /// Activate Game Sounds.
    /// </summary>
    public void GameSoundsOn()
    {
        gameSounds.enabled = true;

        gameSoundOffButton.SetActive(true);
        gameSoundOnButton.SetActive(false);

        GameData.Instance.gameSoundIsMuted = false;  
    }

    /// <summary>
    /// Deactivate Backgrounds Sounds.
    /// </summary>
    public void BackgroundSoundsOff()
    {
        backgroundSound.volume = 0;

        backgroundSoundOffButton.SetActive(false);
        backgroundSoundOnButton.SetActive(true);

        GameData.Instance.backgroundSoundIsMuted = true;
    }

    /// <summary>
    /// Activate Backgrounds Sounds.
    /// </summary>
    public void BackgroundSoundsOn()
    {
        backgroundSound.volume = 0.5f;

        backgroundSoundOffButton.SetActive(true);
        backgroundSoundOnButton.SetActive(false);

        GameData.Instance.backgroundSoundIsMuted = false;
    }
}
