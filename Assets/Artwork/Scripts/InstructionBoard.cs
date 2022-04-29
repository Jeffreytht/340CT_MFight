using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionBoard : MonoBehaviour
{
    public void BackButton()
    {
        SceneManager.LoadScene("lobby");
    }
}
