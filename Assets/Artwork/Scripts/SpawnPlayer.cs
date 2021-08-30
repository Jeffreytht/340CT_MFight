using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    private int minX = int.MaxValue;
    private int maxX = int.MinValue;
    private int minY = int.MaxValue;
    private int maxY = int.MinValue;
    public Tilemap tileMap;

    // Start is called before the first frame update
    private void Start()
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

    public GameObject SpawnNewPlayer()
    { 
        int localX = PhotonNetwork.IsMasterClient ? minX : maxX;
        int localY = minY;

        Vector3 bl = tileMap.CellToWorld(new Vector3Int(localX, localY, (int)tileMap.transform.position.y));
        Vector3 tr = tileMap.CellToWorld(new Vector3Int(localX + 1, localY + 1, (int)tileMap.transform.position.y));

        float x = (bl.x + tr.x) / 2;
        float y = (bl.y + tr.y) / 2;

        string name = SkinRepo.GetObject((int)(PhotonNetwork.LocalPlayer.CustomProperties["skin"])).name;
        return PhotonNetwork.Instantiate(name, new Vector2(x, y), Quaternion.identity);
    }
}
