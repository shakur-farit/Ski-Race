using UnityEngine;

/// <summary>
/// Class for race start line.
/// </summary>
public class StartFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Events.StartRace();
        }
    }
}
