using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject SpawnNewPlayer(float x, float y)
    { 
        string name = SkinRepo.GetObject((int)(PhotonNetwork.LocalPlayer.CustomProperties["skin"])).name;
        return PhotonNetwork.Instantiate(name, new Vector2(x, y), Quaternion.identity);
    }
}
    