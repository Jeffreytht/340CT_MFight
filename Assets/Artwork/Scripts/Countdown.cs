using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public int countdownTime;
    public TMP_Text countdownText;

    IEnumerator CountdownToStart()
    {
        while(countdownTime>0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }
        countdownText.text = "GO!";
       // GameController.instance.BeginGame();
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownToStart());
    }
}
