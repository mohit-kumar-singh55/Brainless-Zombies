using UnityEngine;

public class GameOverZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Zombie))
        {
            // Game over
            Time.timeScale = 0;
        }
    }
}
