using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    public TilemapController tileMapController;
    public int spawnInterval;

    private bool isSpawnCoinEnable;


    public void StartSpawnCoin()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        isSpawnCoinEnable = true;
        StartCoroutine(StartGame());
    }

    public void StopSpawnCoin()
    {
        isSpawnCoinEnable = false;
    }

    IEnumerator StartGame()
    {
        for (int i = 0;  isSpawnCoinEnable; i++)
        {
            yield return new WaitForSeconds(spawnInterval);
            if (isSpawnCoinEnable)
            {
                List<int> tlbr = tileMapController.GetLocalTLBR();
                int x = Random.Range(tlbr[1], tlbr[3]);
                int y = Random.Range(tlbr[2], tlbr[0]);

                List<float> bl = tileMapController.LocalToWorld(x, y);
                List<float> tr = tileMapController.LocalToWorld(x + 1, y + 1);

                Vector2 pos = new Vector2((bl[0] + tr[0]) / 2, (bl[1] + tr[1]) / 2);
                GameObject obj = PhotonNetwork.InstantiateRoomObject(coinPrefab.name, pos, Quaternion.identity);

                Coin.Operator op = Coin.Operator.Addition;
                int operand = 1;

                if (i > 0 && i %5 == 0)
                {
                    op = Random.Range(0, 2) == 0 ? Coin.Operator.Multiplication : Coin.Operator.Division;
                    operand = Random.Range(2, 4);
                }
                else
                {
                    op = Random.Range(0, 2) == 0 ? Coin.Operator.Addition : Coin.Operator.Subtraction;
                    operand = Random.Range(1, 100);
                }

                PhotonView photonView = PhotonView.Get(obj.GetComponent<Coin>());
                photonView.RPC("SetValue", RpcTarget.All, op, operand);
            }
        }
    }
}
