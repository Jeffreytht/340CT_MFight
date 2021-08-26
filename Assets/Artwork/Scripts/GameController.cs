using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public int countdownTime;
    public int gameTime;

    public TMP_Text countdownText;
    public TMP_Text timerText;
    public SpawnCoin coinGenerator;


    // Start is called before the first frame update
    void Start()
    {
        timerText.SetText(secToString(gameTime));
        StartCoroutine(StartCountDown());
    }

    IEnumerator StartCountDown()
    {
        countdownText.gameObject.SetActive(true);

        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        StartCoroutine(StartGame());
    }

    IEnumerator StartGame()
    {
        coinGenerator.StartSpawnCoin();
        for (int i = gameTime; i >= 0; i--)
        {
            timerText.SetText(secToString(i));
            timerText.color = i <= 10 ? Color.red : Color.white;
            yield return new WaitForSeconds(1f);
        }

        timerText.SetText("");
        coinGenerator.StopSpawnCoin();

        countdownText.gameObject.SetActive(true);
        countdownText.text = "Time Up";
    }

    private string secToString(int sec)
    {
        int minute = Mathf.FloorToInt(sec / 60);//get minute
        int second = Mathf.FloorToInt(sec % 60);//get second
        return string.Format("Time: {0:00}:{1:00}", minute, second); //display timer
    }

}
