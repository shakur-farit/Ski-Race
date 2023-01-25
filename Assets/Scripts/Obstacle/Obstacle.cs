using UnityEngine;

/// <summary>
/// Obstacle parent class.
/// </summary>
public class Obstacle : MonoBehaviour
{
    protected PlayerDamage playerDamage;
    protected virtual void OnCollisionEnter(Collision collision)
    {
        playerDamage = collision.gameObject.GetComponent<PlayerDamage>();
        playerDamage.HitDamage = 1;

        if (collision.gameObject.tag == "Player")
        {
            HitToPlayer();
        }   
    }

    /// <summary>
    /// Call HitPlayer() event.
    /// </summary>
    protected virtual void HitToPlayer()
    {
        Events.HitPlayer();
    }
}
