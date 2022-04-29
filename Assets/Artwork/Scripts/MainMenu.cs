using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        AudioController.getInstance().PlaySong(AudioController.GameState.Menu);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("loading");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
