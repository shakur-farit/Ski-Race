using System.Collections;
using UnityEngine;

/// <summary>
/// Class for colliding object with some boosts(bufs)/score.
/// </summary>

// Boosts which player set when collide with this game object.
public enum Boost
{
    Speed,
    None
}

public class WaypointFlag : MonoBehaviour
{
    [Header("Boost Setting")]
    [Tooltip("Which boost have object.")]
    public Boost boost = Boost.None;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            // Get Renderer component from child of this object for change collor.
            Renderer inChild = GetComponentInChildren<Renderer>();
            // Change color from fixed to yellow of child object when player collide with this object.
            inChild.material.color = Color.yellow;

            // Increase the number of Score.
            Events.IncreaseScore();

            if(boost == Boost.Speed)
            {               
                StartCoroutine(SpeedUP(other));
            }
        }
    }

    /// <summary>
    /// Increase the speed and maximum speed of player to (5) seconds.
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    IEnumerator SpeedUP(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();
        player.playerStats.speed += 500;
        player.playerStats.speedMaximum += 500;

        yield return new WaitForSeconds(5f);

        player.playerStats.speed -= 500;
        player.playerStats.speedMaximum -= 500;
    }
}
