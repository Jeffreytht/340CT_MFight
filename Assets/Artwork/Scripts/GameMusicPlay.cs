using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicPlay : MonoBehaviour
{
    public static GameMusicPlay gameMusicPlay;
    void Awake()
    {
        if(gameMusicPlay==null)
        {
            gameMusicPlay=this;
        }
        if(MainMenuMusicPlay.mainMenuMusicPlay!=this)
        {
            Destroy(MainMenuMusicPlay.mainMenuMusicPlay.gameObject);
        }

    }
}
