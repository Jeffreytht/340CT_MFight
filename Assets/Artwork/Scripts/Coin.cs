using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Coin : MonoBehaviour
{
    public TextMeshProUGUI text;

    public enum Operator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    private Operator _operator = Operator.Addition;
    private int _operand = 1;

    public void SetValue(Operator op, int operand)
    {
        _operator = op;
        _operand = operand;
        text.SetText("" + GetOperatorStr(_operator) + _operand);
    }

    private char GetOperatorStr(Operator op)
    {
        switch (op)
        {
            case Operator.Addition:
                return '+';
            case Operator.Division:
                return '÷';
            case Operator.Multiplication:
                return 'x';
            default:
                return '-';
        }
    }
}
