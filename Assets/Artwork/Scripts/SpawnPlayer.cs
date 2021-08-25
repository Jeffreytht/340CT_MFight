using System.Collections;
using System.Collections.Generic;
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
    public GameObject playerPrefab;

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

        Vector3 bl = tileMap.CellToWorld(new Vector3Int(minX, minY, (int)tileMap.transform.position.y));
        Vector3 tr = tileMap.CellToWorld(new Vector3Int(minX + 1, minY + 1, (int)tileMap.transform.position.y));

        float x = (bl.x + tr.x) / 2;
        float y = (bl.y + tr.y) / 2;

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector2(x, y), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
