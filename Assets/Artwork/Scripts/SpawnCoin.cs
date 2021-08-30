using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class SpawnCoin : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap tileMap;
    public int spawnInterval;

    private bool isSpawnCoinEnable;
    private int minX = int.MaxValue;
    private int maxX = int.MinValue;
    private int minY = int.MaxValue;
    private int maxY = int.MinValue;

    // Start is called before the first frame update
    void Start()
    {
        for (int n = tileMap.cellBounds.xMin; n < tileMap.cellBounds.xMax; n++)
        {
            for (int p = tileMap.cellBounds.yMin; p < tileMap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)tileMap.transform.position.y);
                if (tileMap.HasTile(localPlace))
                {
                    minX = Mathf.Min(minX, n);
                    maxX = Mathf.Max(maxX, n);
                    minY = Mathf.Min(minY, p);
                    maxY = Mathf.Max(maxY, p);
                }
            }
        }
    }

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
                int x = Random.Range(minX, maxX);
                int y = Random.Range(minY, maxY);

                Vector3 bl = tileMap.CellToWorld(new Vector3Int(x, y, (int)tileMap.transform.position.y));
                Vector3 tr = tileMap.CellToWorld(new Vector3Int(x + 1, y + 1, (int)tileMap.transform.position.y));

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

                PhotonView photonView = PhotonView.Get(obj.GetComponent<Coin>());
                photonView.RPC("SetValue", RpcTarget.All, op, operand);
            }
        }
    }
}
