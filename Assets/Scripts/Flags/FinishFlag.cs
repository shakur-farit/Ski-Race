using UnityEngine;

/// <summary>
/// Class for race finish line. 
/// </summary>
public class FinishFlag : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Events.StopRace();           
        }
    }
}
