using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;
using Photon.Pun.UtilityScripts;

public class ScoreController : MonoBehaviour
{
    public TextMeshProUGUI p1Name;
    public TextMeshProUGUI p1Score;
    public TextMeshProUGUI p2Name;
    public TextMeshProUGUI p2Score;
    public Image p1Skin;
    public Image p2Skin;

    private Player player1;
    private Player player2;


    public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public int SkinIdx { get; set; }
    }

    public void Init()
    {
        int localID = PhotonNetwork.LocalPlayer.ActorNumber;
        foreach (var player in PhotonNetwork.PlayerList)
        {
            Player p = new Player();
            p.ID = player.ActorNumber;
            p.Name = player.NickName;
            p.Score = player.GetScore();
            p.SkinIdx = (int)(player.CustomProperties["skin"]);

            if (p.ID == localID)
                player1 = p;
            else
                player2 = p;
        }

        p1Skin.sprite = SkinRepo.GetSprite(player1.SkinIdx);
        p2Skin.sprite = SkinRepo.GetSprite(player2.SkinIdx);
        p1Name.SetText(player1.Name);
        p2Name.SetText(player2.Name);
        SetScore(p1Score, player1.Score);
        SetScore(p2Score, player2.Score);
    }

    void SetScore(TextMeshProUGUI text, int score)
    {
        text.SetText($"Score : {score}");
    }

    [PunRPC]
    public void UpdateScore()
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player1.ID == player.ActorNumber)
                player1.Score = player.GetScore();
            else
                player2.Score = player.GetScore();
        }
    
        SetScore(p1Score, player1.Score);
        SetScore(p2Score, player2.Score);
    }
}
