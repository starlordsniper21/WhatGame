using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedTime;
    bool gameIsOver = false;
    public GameOverManager gameOverManager;


    private void Update()
    {
        if (!gameIsOver)
        {
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);

            if (minutes >= 1)
            {
                gameIsOver = true;

                if(gameIsOver != null)
                {
                    gameOverManager.gameOver();
                }

            }
            else
            {
                if (minutes == 00 && seconds == 50) 
                {
                    timerText.color = Color.red;
                }
                else
                {
                    timerText.color = Color.white;
                }
                timerText.text = string.Format("{00:00}:{1:00}", minutes, seconds);
            }



        }
    }

}
