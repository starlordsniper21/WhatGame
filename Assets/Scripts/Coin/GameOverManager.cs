using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverUi;
    public GameObject leaderboardUi; // Reference to the leaderboard UI panel
    private bool leaderboardActive = false; // Track if leaderboard is active

    public TMP_Text currentScoreText; // Reference to the TMP_Text component for displaying current score

    public void gameOver()
    {
        gameOverUi.SetActive(true);
    }

    public void restart()
    {
        // Reset the score before restarting the game
        CoinCounter.Instance.ResetScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void toggleLeaderboard()
    {
        leaderboardActive = !leaderboardActive;
        leaderboardUi.SetActive(leaderboardActive);

        // Check if the leaderboard UI panel is not active before resetting the score
        if (!leaderboardActive)
        {
            CoinCounter.Instance.ResetScore();
        }
    }

    public void menu()
    {
        // Check if the leaderboard UI panel is not active before resetting the score
        if (!leaderboardActive)
        {
            CoinCounter.Instance.ResetScore();
        }

        SceneManager.LoadScene("Main Menu");
    }

    // Call this method to update the current score text in the leaderboard UI
    public void UpdateCurrentScore(int score)
    {
        if (currentScoreText != null)
        {
            currentScoreText.text = score.ToString();
        }
    }
}
