using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

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

    [PunRPC]
    public void SetValue(Operator op, int operand)
    {
        _operator = op;
        _operand = operand;
        text.SetText(GetOperatorStr(_operator) + _operand);
    }

    public int Operand {
        get
        {
            return _operand;
        }
    }

    public Operator Op
    {
        get
        {
            return _operator;
        }
    }

    public static string GetOperatorStr(Operator op)
    {
        switch (op)
        {
            case Operator.Addition:
                return "+";
            case Operator.Division:
                return "÷";
            case Operator.Multiplication:
                return "x";
            default:
                return "-";
        }
    }

    public bool IsPenalty()
    {
        return _operator == Operator.Division || _operator == Operator.Subtraction;
    }

    [PunRPC]
    public void DestroyCoin()
    {
        Destroy(gameObject);
    }
}
