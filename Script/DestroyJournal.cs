using UnityEngine;

public class DestroyJournalOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            Destroy(gameObject); // Destroy the journal object when the player collides with it
        }
    }
}
