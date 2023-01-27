using UnityEngine;

/// <summary>
/// Checks of player grounded.
/// </summary>
public class GroundCheck : MonoBehaviour
{
    public bool isGrounded = false;

    // Set true to isGrounded when player collide with "Ground" taged object.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            print("On ground");
        }
    }

    // Set false to isGrounded when player exit from collider of "Ground" taged object.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false; 
            print("Not on ground");
        }
    }
}
