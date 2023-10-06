using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI inputScore;
    [SerializeField] private TMP_InputField inputName;

    public UnityEvent<string, int> submitScoreEvent;
    public CoinCounter coinCounter;

    public void SubmitScore()
    {
        string scoreInput = inputScore.text;
        scoreInput = scoreInput.Trim();

        if (string.IsNullOrWhiteSpace(scoreInput))
        {
            Debug.LogError("Score input is empty or whitespace.");
            return;
        }

        if (!int.TryParse(scoreInput, out int score))
        {
            Debug.LogError("Invalid input for score: " + scoreInput);
            return;
        }

        string playerName = inputName.text;
        submitScoreEvent.Invoke(playerName, score);
    }



    //public void SaveScore()
    //{
    //    int scoreToSave = coinCounter.currentCoin;
    //    PlayerPrefs.SetInt("PlayerScore", scoreToSave);
    //    PlayerPrefs.Save();
    //    Debug.Log("Score saved: " + scoreToSave);
    //}
}
