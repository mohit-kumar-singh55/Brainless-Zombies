using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class ResultScore : MonoBehaviour
{
    TMP_Text scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        int score = PlayerPrefs.GetInt(PlayerPrefsValues.PLAYER_SCORE, 0);
        scoreText.text =score + "";
    }
}
