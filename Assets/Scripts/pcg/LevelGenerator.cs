using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class LevelGenerator : MonoBehaviour
{
    // public Tilemap tilemap;
    // public TileBase tile;
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
        grid = new GameObject("Grid_0").AddComponent<Grid>();

        for (int i = 0 ; i <= tiles.GetUpperBound(0); i++) {
            var tilemap = CreateTilemap("Tilemap_"+i);
            tilemap.ClearAllTiles();
            GenerateMap(tilemap, tiles[i]);
        }
    }

    private Tilemap CreateTilemap(string name)
    {
            var gameObject = new GameObject(name);
            var tilemap = gameObject.AddComponent<Tilemap>();
            var tilemapRenderer = gameObject.AddComponent<TilemapRenderer>();

            tilemap.tileAnchor = new Vector3(0,0,0);
            gameObject.transform.SetParent(grid.transform);
            tilemapRenderer.sortingLayerName = "Main";

            return tilemap;
    }

    private string LogMap(int[,] map)
    {
        var str = "";
        for (int y = map.GetUpperBound(1); y > 0; y--) {
            for (int x = 0; x < map.GetUpperBound(0); x++) {
                str = str + map[x,y] + ", ";
            }
            str = str + "\n";
        }
        return str;
    }

    public void GenerateMap(Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles();

        if (randomSeed) {
            seed = Random.Range(0f,10f);
        }

        int[,] map = new int[width, height];
        map = MapHelpers.GenerateArray(width, height, true);
        Debug.Log("GenerateArray:\n" + LogMap(map));

        map = MapHelpers.PerlinNoise(map, seed);
        Debug.Log("PerlinNoise:\n" + LogMap(map));
        MapHelpers.RenderMap(map, tilemap, tile);
    }
}