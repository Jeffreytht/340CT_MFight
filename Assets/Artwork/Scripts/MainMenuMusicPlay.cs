using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusicPlay: MonoBehaviour
{
    public static MainMenuMusicPlay mainMenuMusicPlay;

    void Awake()
    {
        if(mainMenuMusicPlay==null)
        {
            mainMenuMusicPlay=this;
        }
        else if(mainMenuMusicPlay!=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
}
