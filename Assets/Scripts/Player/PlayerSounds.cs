using UnityEngine;

/// <summary>
/// Class of playing sounds clips related to the Player.
/// </summary>
public class PlayerSounds : MonoBehaviour
{
    [Tooltip("Sound to play when the player is hit")]
    public AudioClip collisionSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Events.OnHitPlayer += PlayCollisionSound;
    }

    private void OnDisable()
    {
        Events.OnHitPlayer -= PlayCollisionSound;
    }

    /// <summary>
    /// Play collisionSound when the Player take damage.
    /// </summary>
    private void PlayCollisionSound()
    {
        audioSource.PlayOneShot(collisionSound);
    }
}
