using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TextMeshProUGUI createErrMsgText;
    public TextMeshProUGUI joinErrMsgText;

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(createInput.text))
        {
            createErrMsgText.SetText("Please enter a room name");
            Debug.Log("Create room failed");
        }
        else{
            createErrMsgText.SetText(""); 
            PhotonNetwork.CreateRoom(createInput.text);
        }
    }

    public void JoinRoom()
    {
        if(string.IsNullOrEmpty(joinInput.text))
        {
            joinErrMsgText.SetText("Please enter a room name");
        }
        else{
            joinErrMsgText.SetText(""); 
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
            PhotonNetwork.LoadLevel("game");
        else
            PhotonNetwork.LoadLevel("waitingOpponent");
    }

    public void UpdateCreateErrMsg() {
        createErrMsgText.SetText("");
    }

    public void UpdateJoinErrMsg() {
        joinErrMsgText.SetText("");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        createErrMsgText.SetText(message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        joinErrMsgText.SetText(message);
    }
}
