using UnityEngine;

/// <summary>
/// Class of playing sounds of game (hits, clicks, etc.).
/// </summary>
public class GameSounds : DontDestroyOnLoadObject<GameSounds>
{
    [Header ("Audio Clip Components")]
    [Space(10)]
    [Tooltip("Sound to play when the player is hit.")]
    public AudioClip collisionSound;
    [Tooltip("Sound of click of buttons.")]
    public AudioClip clickSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Events.OnHitPlayer += PlayHitSound;
        Events.OnClickButton += PlayButtonClickSound;
    }

    private void OnDisable()
    {
        Events.OnHitPlayer -= PlayHitSound;
        Events.OnClickButton -= PlayButtonClickSound;
    }

    public void PlayButtonClickSound()
    { 
        audioSource.PlayOneShot(clickSound);
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
