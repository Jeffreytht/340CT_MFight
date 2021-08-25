using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    Text timerText;//text to display the timer
    public float timeRemaining = 120;
    public bool timerIsRunning = false;
	
    // Start is called before the first frame update
    void Start()
    {
        //starts the timer automatically
        timerIsRunning = true;
        timerText = GetComponent<Text>();//get the timer text   
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsRunning)
        {
            if(timeRemaining>0)
            {
                timeRemaining -= Time.deltaTime;//count down the time
                DisplayTime(timeRemaining);
            }
            else{
                //when times up
                timerText.text="Time Up";
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    //display time
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minute = Mathf.FloorToInt(timeToDisplay/60);//get minute
        float second = Mathf.FloorToInt(timeToDisplay%60);//get second
        if(minute==0&&second<=10)
        {
            timerText.color = Color.red;
        }
        else{
            timerText.color = Color.white;
        }
        timerText.text = string.Format("Time: {0:00}:{1:00}",minute,second);//display timer
    }
}
