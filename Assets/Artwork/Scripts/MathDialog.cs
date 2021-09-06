using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MathDialog : MonoBehaviour
{
    public TextMeshProUGUI ansLeft;
    public TextMeshProUGUI ansRight;
    public TextMeshProUGUI question;

    private int correctAnsIdx;
    private int correctAns;
    private int score;
    private bool isPenalty;

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
    }

    public void Close()
    {
        GetComponent<Canvas>().enabled = false;
    }


    public void AnsLeftSelected()
    {
        OnPlayerScoreChanged(isPenalty, correctAnsIdx == 0 ? correctAns : score);
        Close();
    }

    public void AnsRightSelected()
    {
        OnPlayerScoreChanged(isPenalty, correctAnsIdx == 1 ? correctAns : score);
        Close();
    }

    public void SetOnPlayerScoreChangedListener(OnPlayerScoreChanged listener)
    {
        OnPlayerScoreChanged = listener;
    }
}
