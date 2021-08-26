using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WaitOpponent : MonoBehaviourPunCallbacks
{
    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            PhotonNetwork.LoadLevel("game");
    }
}
