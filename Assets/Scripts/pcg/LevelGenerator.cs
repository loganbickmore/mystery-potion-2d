using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelGenerator : MonoBehaviour
{
    // public Tilemap tilemap;
    public TileBase tile;
    public int width;
    public int height;
    public float seed;
    public bool randomSeed = true;


    private Grid grid; // = new GameObject("Grid0").AddComponent<Grid>();
    // private Grid grid = new GameObject("Grid").AddComponent<Grid>();
    private Tilemap[] tilemaps;
    public TileBase[] tiles;

    void Start()
    {
        // GenerateMap();

        if (randomSeed) {
            seed = Time.time;
        }
        
        grid = new GameObject("Grid0").AddComponent<Grid>();

        // clear tiles
        for (int i = 0 ; i <= tiles.GetUpperBound(0); i++) {
            var go = new GameObject("Tilemap"+i);
            var tm = go.AddComponent<Tilemap>();
            var tr = go.AddComponent<TilemapRenderer>();

            tm.tileAnchor = new Vector3(0,0,0);
            go.transform.SetParent(grid.transform);
            tr.sortingLayerName = "Main";

            tm.ClearAllTiles();
            GenerateMap(tm, tiles[i]);

        }

        


    }

    public void GenerateMap(Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();

        if (randomSeed) {
            seed = Random.Range(0f,10f);
        }

        int[,] map = new int[width, height];
        map = MapHelpers.GenerateArray(width, height, true);
        map = MapHelpers.PerlinNoise(map, seed);
        MapHelpers.RenderMap(map, tilemap, tile);
    }
}