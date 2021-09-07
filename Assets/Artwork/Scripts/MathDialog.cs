using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathDialog : MonoBehaviour
{
    public TextMeshProUGUI ansLeft;
    public TextMeshProUGUI ansRight;
    public TextMeshProUGUI question;
    public TextMeshProUGUI timerText;
    public Button leftButton;
    public Button rightButton;
    public Slider slider;
    public int timeToAnswer = 12;

    private int correctAnsIdx;
    private int correctAns;
    private int score;
    private bool isPenalty;
    private IEnumerator timer;

    private OnPlayerScoreChanged OnPlayerScoreChanged;

    public void SetQuestion(int score, Coin.Operator op, int operand, bool isPenalty)
    {
        this.score = score;
        this.isPenalty = isPenalty;

        switch (op)
        {
            case Coin.Operator.Addition:
                correctAns = score + operand;
                break;
            case Coin.Operator.Subtraction:
                correctAns = score - operand;
                break;
            case Coin.Operator.Multiplication:
                correctAns = score * operand;
                break;
            case Coin.Operator.Division:
                correctAns = score / operand;
                score = correctAns * operand;
                break;
        }

        question.SetText($"{score} {Coin.GetOperatorStr(op)} {operand}");
        int wrongAns = Random.Range(1, 20) * (Random.Range(0, 1) == 1 ? -1 : 1) + correctAns;

        if (Random.Range(0, 1) == 0)
        {
            ansLeft.SetText("{0}", correctAns);
            ansRight.SetText("{0}", wrongAns);
            correctAnsIdx = 0;
        }
        else
        {
            ansRight.SetText("{0}", correctAns);
            ansLeft.SetText("{0}", wrongAns);
            correctAnsIdx = 1;
        }
    }

    public void Exec()
    {
        GetComponent<Canvas>().enabled = true;
        timer = StartTimer();
        StartCoroutine(timer);
    }

    IEnumerator StartTimer()
    {
        for (float i = timeToAnswer; i > 0; i -= 0.1f)
        {
            timerText.SetText(((int)i).ToString() + " sec");
            slider.value = i / timeToAnswer;
            yield return new WaitForSeconds(0.1f);
        }

        OnPlayerScoreChanged(isPenalty, score);
        Close();
    }

    public void Close()
    {
        GetComponent<Canvas>().enabled = false;
        if (timer != null)
            StopCoroutine(timer);
    }

    public void AnsLeftSelected()
    {
        if(correctAnsIdx==0)
        {
            leftButton.image.color = Color.green;
            rightButton.image.color = Color.red;
        }
        else{
            leftButton.image.color = Color.red;
            rightButton.image.color = Color.green;
        }
        OnPlayerScoreChanged(isPenalty, correctAnsIdx == 0 ? correctAns : score);
        StartCoroutine(closeDialog());
        
    }

    public void AnsRightSelected()
    {
        if(correctAnsIdx==1)
        {
            leftButton.image.color = Color.red;
            rightButton.image.color = Color.green;
        }
        else{
            leftButton.image.color = Color.green;
            rightButton.image.color = Color.red;
        }
        OnPlayerScoreChanged(isPenalty, correctAnsIdx == 1 ? correctAns : score);
        StartCoroutine(closeDialog());

    }

    IEnumerator closeDialog()
    {
        yield return new WaitForSeconds(1);
        //set the button back to default color
        Color defaultColor = new Color();
        ColorUtility.TryParseHtmlString ("#F59F38", out defaultColor);
        leftButton.image.color =  defaultColor;
        rightButton.image.color = defaultColor;

        Close();
    }

    public void SetOnPlayerScoreChangedListener(OnPlayerScoreChanged listener)
    {
        OnPlayerScoreChanged = listener;
    }
}
