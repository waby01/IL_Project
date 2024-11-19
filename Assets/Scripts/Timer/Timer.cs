using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    [SerializeField] float remainingTime;

    public GameOverScreen gameOverScreen;

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0 )
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 4)
            {
                timerText.color = Color.red;

            }

        }
         else if (remainingTime <= 0) {
            remainingTime = 0;
            Time.timeScale = 0;
            gameOver();
          


        }
       
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);

    }

    public void gameOver()
    {
     
        gameOverScreen.setup();
        



    }
}
