using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Zombie))
        {
            // Game over
            SceneManager.LoadScene("ResultScene");
            
            PlayerPrefs.SetInt(PlayerPrefsValues.PLAYER_SCORE,ScoreSystem.Instance.Score);
            PlayerPrefs.Save();
        }
    }
}
