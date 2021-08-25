using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeedGenerator : MonoBehaviour
{
    public GameObject coinPrefab;
    public Tilemap tileMap;
    public float respawnTime = 2.0f;

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

        StartCoroutine(StartGame());
    }

    private void SpawnCoin()
    {
        int x = Random.Range(minX, maxX);
        int y = Random.Range(minY, maxY);

        Vector3 bl = tileMap.CellToWorld(new Vector3Int(x, y, (int)tileMap.transform.position.y));
        Vector3 tr = tileMap.CellToWorld(new Vector3Int(x + 1, y + 1, (int)tileMap.transform.position.y));

        GameObject coin = Instantiate(coinPrefab) as GameObject;
        coin.transform.position = new Vector2((bl.x + tr.x) / 2, (bl.y + tr.y) / 2);
    }


    IEnumerator StartGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnCoin();
        }
    }
}