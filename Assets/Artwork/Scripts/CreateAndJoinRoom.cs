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

    public void CreateRoom(GameObject errorWindowCreate)
    {
        if(string.IsNullOrEmpty(createInput.text))
        {
            errorWindowCreate.SetActive(true); 
            Debug.Log("Please enter an room name");
        }
        else{
            errorWindowCreate.SetActive(false); 
            PhotonNetwork.CreateRoom(createInput.text);
        }
    }

    public void JoinRoom(GameObject errorWindowJoin)
    {
        if(string.IsNullOrEmpty(joinInput.text))
        {
            errorWindowJoin.SetActive(true); 
            Debug.Log("Please enter an room name");
        }
        else{
            errorWindowJoin.SetActive(false); 
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public void DisplayErrorCreateMessage(GameObject errorWindowCreate) {
    
        if (createInput.text != "") { 
             errorWindowCreate.SetActive(false); 

        }
    }
    public void DisplayErrorJoinMessage(GameObject errorWindowJoin) {
    
        if (joinInput.text != "") { 
             errorWindowJoin.SetActive(false); 

        }
    }
}
