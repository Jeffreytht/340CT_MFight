using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderBroad : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;

    // Start is called before the first frame update
    void Start()
    {
        AudioController.getInstance().PlaySong(AudioController.GameState.Menu);
        List<Players> playerList= new List<Players>();
        int testingScore=1000;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            player.CustomProperties["score"]=testingScore;
            testingScore+=1000;
            Players players = new Players(player.NickName,(int)(player.CustomProperties["score"]));
            playerList.Add(players);
        }
        playerList.Sort((p1, p2) => p2.score-p1.score);

       
        int placeCount =1;
        foreach (Players player in playerList)
        {
            GameObject rowObject = Instantiate(rowPrefab,rowsParent);
            TMP_Text[] texts = rowObject.GetComponentsInChildren<TMP_Text>();
            texts[0].text=placeCount.ToString();
            texts[1].text=player.name;
            texts[2].text=player.score.ToString();
            placeCount++;
        }
        
    }

    // Update is called once per frame
    public void backToLobby()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("lobby");
    }
  
}
public class Players
{
    public string name;
    public int score;
    public Players(string name,int score)
    {
        this.name=name;
        this.score=score;
    }
}
