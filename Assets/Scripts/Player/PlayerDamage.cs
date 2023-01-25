using System.Collections;
using UnityEngine;

/// <summary>
/// Class of dealing damage to Player.
/// </summary>
public class PlayerDamage : MonoBehaviour
{
    [Tooltip("How much force knocks the player backwards after crashing into obstacle.")]
    [SerializeField] private float knockbackForce;

    [Tooltip("How many seconds before the player can move downhill again after crashing into an obstacle.")]
    [SerializeField] private float recoveryTime;

    [Tooltip("How many Player take with hit.")]
    [SerializeField] private int hitDamage;

    [Tooltip("Checks when the player is hurt.")]
    public bool isHurt;

    private Events events;
    private Rigidbody rb;

    public int HitDamage { get { return hitDamage; } set { hitDamage = value; } }

    // Register that TakeDamage will be called when an OnPlayerHit Event happens.
    private void OnEnable()
    {
        Events.OnHitPlayer += TakeDamage;
    }

    private void OnDisable()
    {
        Events.OnHitPlayer -= TakeDamage;
    }

    private void Start()
    {
        events = GetComponent<Events>();
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Throw away the Player when he collide with Obstacle/Enemies.
    /// </summary>
    private void TakeDamage()
    {
        if(!isHurt)
        {
            isHurt = true;
            rb.velocity = Vector3.zero;

            // Deal damage to Player
            Health playerHealth = GetComponent<Health>();
            playerHealth.ChangeCurrentHealth(-hitDamage);

            // Sends the player up and back from bumping into an obstacle
            rb.AddForce(transform.forward * -knockbackForce);
            rb.AddForce(transform.up * 500);
            StartCoroutine(Recover());

            if (playerHealth.CurrentHealth <= 0)
            {
                events.ShowGameOverPanel();
            }
        }
    }

    /// <summary>
    /// Change isHurt to true after recoveryTime to be able to TakeDamage() again.
    /// </summary>
    /// <returns></returns>
    IEnumerator Recover()
    {
        yield return new WaitForSeconds(recoveryTime);
        isHurt = false;
    }
}
