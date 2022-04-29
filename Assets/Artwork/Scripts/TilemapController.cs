using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;

public class TilemapController : MonoBehaviour
{
    private int minX = int.MaxValue;
    private int maxX = int.MinValue;
    private int minY = int.MaxValue;
    private int maxY = int.MinValue;
    public Tilemap floorTilemap;
    public Tilemap wallTilemap;

    private Dictionary<int, Dictionary<int, bool>> availableTile = new Dictionary<int, Dictionary<int, bool>>();

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize floor tile
        for (int n = floorTilemap.cellBounds.xMin; n < floorTilemap.cellBounds.xMax; n++)
        {
            for (int p = floorTilemap.cellBounds.yMin; p < floorTilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = new Vector3Int(n, p, (int)floorTilemap.transform.position.z);
                if (floorTilemap.HasTile(localPlace))
                {
                    minX = Mathf.Min(minX, n);
                    maxX = Mathf.Max(maxX, n);
                    minY = Mathf.Min(minY, p);
                    maxY = Mathf.Max(maxY, p);

                    if (!availableTile.ContainsKey(p))
                        availableTile.Add(p, new Dictionary<int, bool>());

                    availableTile[p].Add(n, true);
                }
            }
        }

        // Remove floor tile with wall
        for (int n = wallTilemap.cellBounds.xMin; n < wallTilemap.cellBounds.xMax; n++)
        {
            for (int p = wallTilemap.cellBounds.yMin; p < wallTilemap.cellBounds.yMax; p++)
            {
                Vector3Int wallCellPos = new Vector3Int(n, p, (int)floorTilemap.transform.position.z);
                Vector3 wallWorldPos = wallTilemap.CellToWorld(wallCellPos);
                Vector3Int floorCellPos = WorldToFloorCell(wallWorldPos.x, wallWorldPos.y);

                if (wallTilemap.HasTile(wallCellPos) && floorTilemap.HasTile(floorCellPos))
                {
                    if (availableTile.ContainsKey(p) && availableTile[p].ContainsKey(n))
                        availableTile[p][n] = false;
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
        Vector3 bl = floorTilemap.CellToWorld(new Vector3Int(minX, maxY, (int)floorTilemap.transform.position.z));
        Vector3 tr = floorTilemap.CellToWorld(new Vector3Int(maxX, minY, (int)floorTilemap.transform.position.z));

        return new List<float> { tr.y, bl.x, bl.y, tr.x };
    }

    public Vector3 FloorCellToWorld(int x, int y)
    {
        return floorTilemap.CellToWorld(new Vector3Int(x, y, (int)floorTilemap.transform.position.z));
    }

    public Vector3Int WorldToFloorCell(float x, float y)
    {
        return floorTilemap.WorldToCell(new Vector3(x, y, (float)floorTilemap.transform.position.z));
    }

    [PunRPC]
    public bool AllocateIfCellAvailable(int x, int y)
    {
        if (!(availableTile.ContainsKey(y) && availableTile[y].ContainsKey(x)))
        {
            return false;
        }

        if (availableTile[y][x] == false)
        {
            return false;
        }

        availableTile[y][x] = false;
        return true;
    }

    [PunRPC]
    public bool FreeFloorCell(int x, int y)
    {
        if (!(availableTile.ContainsKey(y) && availableTile[y].ContainsKey(x)))
        {
            return false;
        }

        if (availableTile[y][x] == false)
        {
            return false;
        }

        availableTile[y][x] = true;
        return true;
    }

}
