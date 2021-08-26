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
    public TMP_InputField nameInput;
    public GameObject nameObject;
 

    public void CreateRoom(GameObject errorWindowCreate)
    {
        if(string.IsNullOrEmpty(createInput.text.Trim()))
        {
            errorWindowCreate.SetActive(true); 
            Debug.Log("Please enter an room name");
        }
        else if(string.IsNullOrEmpty(nameInput.text.Trim()))
        {
            errorWindowCreate.SetActive(false); 
            Debug.Log("name is empty");
            Vibration.instance.Shake();
        }
        else{
            errorWindowCreate.SetActive(false);
            
            PhotonNetwork.NickName = nameInput.text.Trim();
            PhotonNetwork.CreateRoom(createInput.text);
        }
    }

    public void JoinRoom(GameObject errorWindowJoin)
    {
        if(string.IsNullOrEmpty(joinInput.text.Trim()))
        {
            errorWindowJoin.SetActive(true); 
            Debug.Log("Please enter an room name");
        }
        else if(string.IsNullOrEmpty(nameInput.text.Trim()))
        {
            errorWindowJoin.SetActive(false); 
            Debug.Log("name is empty");
            Vibration.instance.Shake();
        }
        else{
            errorWindowJoin.SetActive(false); 
            PhotonNetwork.NickName = nameInput.text.Trim();
            PhotonNetwork.JoinRoom(joinInput.text);
        }
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("game");
    }

    public void DisplayErrorCreateMessage(GameObject errorWindowCreate) {
    
        if (createInput.text.Trim() != "") { 
             errorWindowCreate.SetActive(false); 

        }
    }
    public void DisplayErrorJoinMessage(GameObject errorWindowJoin) {
    
        if (joinInput.text.Trim() != "") { 
             errorWindowJoin.SetActive(false); 

        }
    }
   
}
