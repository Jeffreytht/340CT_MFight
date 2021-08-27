using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LeaderBroad : MonoBehaviour
{
    public GameObject rowPrefab;
    public Transform rowsParent;

    // Start is called before the first frame update
    void Start()
    {
        AudioController.getInstance().PlaySong(AudioController.GameState.Menu);
         
        foreach (var player in PhotonNetwork.PlayerList)
        {
            GameObject rowObject = Instantiate(rowPrefab,rowsParent);
            player.CustomProperties["score"]=1000;
            TMP_Text[] texts = rowObject.GetComponentsInChildren<TMP_Text>();
            texts[1].text=player.NickName;
            texts[2].text=player.CustomProperties["score"].ToString();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
