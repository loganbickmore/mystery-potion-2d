using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapHelpers
{
    // create a binary matrix
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (empty) {
                    map[x,y] = 0;
                } else {
                    map[x,y] = 1;
                }
            }
        }
        return map;
    }

    // Render our map to the tilemap. Place tiles anywhere there is a 1 in our array
    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (map[x,y] == 1) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }

    // update the map rather than re render for performance
    public static void UpdateMap(int[,] map, Tilemap tilemap)
    {
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            for (int y = 0; y < map.GetUpperBound(1); y++) {
                if (map[x,y] == 0) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), null);
                }
            }
        }
    }

    public static int[,] PerlinNoise(int[,] map, float seed)
    {
        int newPoint;
        float reduction = 0.5f;
        for (int x = 0; x < map.GetUpperBound(0); x++) {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * map.GetUpperBound(1));
            newPoint += (map.GetUpperBound(1) / 2);
            for (int y = newPoint; y >= 0; y--) {
                map[x, y] = 1;
            }
        }
        return map;
    }
}