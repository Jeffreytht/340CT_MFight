using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapController : MonoBehaviour
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

    public List<int> GetLocalTLBR()
    {
        return new List<int> { maxY, minX, minY, maxX };
    }

    public List<float> GetWorldTLBR()
    {
        Vector3 bl = tileMap.CellToWorld(new Vector3Int(minX, maxY, (int)tileMap.transform.position.y));
        Vector3 tr = tileMap.CellToWorld(new Vector3Int(maxX, minY, (int)tileMap.transform.position.y));

        return new List<float> { tr.y, bl.x, bl.y, tr.x };
    }

    public List<float> LocalToWorld(int x, int y)
    {
        Vector3 pos = tileMap.CellToWorld(new Vector3Int(x, y, (int)tileMap.transform.position.y));
        return new List<float> { pos.x, pos.y };
    }

}
