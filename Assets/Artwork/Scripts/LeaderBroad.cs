using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;

public class LeaderBroad : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;

    // Start is called before the first frame update
    void Start()
    {
        AudioController.getInstance().PlaySong(AudioController.GameState.Menu);
        List<Players> playerList = new List<Players>();

        for(int i=0;i<2;i++)
        {
            String NameKey = i+"Name";
            String ScoreKey = i+"Score";

            Players players = new Players(PlayerPrefs.GetString(NameKey),PlayerPrefs.GetInt(ScoreKey));
            playerList.Add(players);
        }

        playerList.Sort((p1, p2) => p2.score - p1.score);

        for (int i = 0,j = 1; i < playerList.Count; i++)
        {
            GameObject rowObject = Instantiate(rowPrefab,rowsParent);
            TMP_Text[] texts = rowObject.GetComponentsInChildren<TMP_Text>();
            if(i>0)
            {
                if(playerList[i].score!=playerList[i-1].score)
                {
                    j++;
                }
            }
            texts[0].text = (j).ToString();
            texts[1].text = playerList[i].name;
            texts[2].text = playerList[i].score.ToString();
        }
        
        
    }

    // Update is called once per frame
    public void backToLobby()
    {
        SceneManager.LoadScene("lobby");
    }
  
}

public class Players
{
    public string name;
    public int score;

    public Players(string name,int score)
    {
        this.name = name;
        this.score = score;
    }
}
