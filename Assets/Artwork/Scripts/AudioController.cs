using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public AudioSource menuSong;
    public AudioSource gameSong;

    public static AudioController getInstance() { return _instance; }

    public enum GameState
    {
        Menu,
        Game,
    }

    private AudioController() {
       
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            DontDestroyOnLoad(menuSong);
            DontDestroyOnLoad(gameSong);
        }
    }

    public void PlaySong(GameState state)
    {
        menuSong.Stop();
        gameSong.Stop();

        if (state == GameState.Game)
            gameSong.Play();
        else
            menuSong.Play();
    }
}
