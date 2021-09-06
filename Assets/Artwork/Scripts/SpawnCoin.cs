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
                int x = 0; 
                int y = 0;

                do
                {
                    x = Random.Range(tlbr[1], tlbr[3]);
                    y = Random.Range(tlbr[2], tlbr[0]);
                } while (!tileMapController.AllocateIfCellAvailable(x, y));

                PhotonView tilemapView = PhotonView.Get(tileMapController.GetComponent<TilemapController>());
                tilemapView.RPC("AllocateIfCellAvailable", RpcTarget.All, x, y);

                Vector3 bl = tileMapController.FloorCellToWorld(x, y);
                Vector3 tr = tileMapController.FloorCellToWorld(x + 1, y + 1);

                Vector2 pos = new Vector2((bl.x + tr.x) / 2, (bl.y + tr.y) / 2);
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

                PhotonView coinView = PhotonView.Get(obj.GetComponent<Coin>());
                coinView.RPC("SetValue", RpcTarget.All, op, operand);
            }
        }
    }
}
