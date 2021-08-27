using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateAndJoinRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField nameInput;

    public TextMeshProUGUI createErrMsgText;
    public TextMeshProUGUI joinErrMsgText;
    public Vibration vibration;
    public int maxNumofPlayer = 2;
 

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(createInput.text.Trim()))
        {
            createErrMsgText.SetText("Please enter a room name");
        }
        else if(string.IsNullOrEmpty(nameInput.text.Trim()))
        {
            vibration.Shake();
        }
        else
        {
            createErrMsgText.SetText("");

            RoomOptions options = new RoomOptions();
            options.MaxPlayers = (byte)maxNumofPlayer;

            PhotonNetwork.NickName = nameInput.text.Trim();
            PhotonNetwork.CreateRoom(createInput.text, options);
        }
    }

    public void JoinRoom()
    {
        if(string.IsNullOrEmpty(joinInput.text.Trim()))
        {
            joinErrMsgText.SetText("Please enter a room name");
        }
        else if(string.IsNullOrEmpty(nameInput.text.Trim()))
        {
            vibration.Shake();
        }
        else
        {
            joinErrMsgText.SetText(""); 
            PhotonNetwork.NickName = nameInput.text.Trim();
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public void BackButton()
    {
        PhotonNetwork.Disconnect ();
        SceneManager.LoadScene("menu");
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
