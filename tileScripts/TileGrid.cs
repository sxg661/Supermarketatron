using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour {

    public static int[] GRIDRANGEX = new int[] { -7, 7 };
    public static int[] GRIDRANGEY = new int[] { -7, 7 };

    public GameObject tilePrefab;

    public static int getSortingNum(int y)
    {
        return (GRIDRANGEY[1] - y);
    }

    public static int getGridSize()
    {
        return (GRIDRANGEX[1] - GRIDRANGEX[0]) * (GRIDRANGEY[1] - GRIDRANGEY[0]);
    }

    public static bool indexInGrid(int x, int y)
    {
        return (x >= GRIDRANGEX[0] && x <= GRIDRANGEX[1]) &&
            (y >= GRIDRANGEX[0] && y <= GRIDRANGEY[1]);
    }

    public static bool inGridBounds(Vector2 bottomLeftPos, float xsize, float ysize)
    {
        float tileEdgex = xsize - 1;
        float tileEdgey = ysize - 1;
        int xtile = Tile.getTile(bottomLeftPos[0]);
        int ytile = Tile.getTile(bottomLeftPos[1]);
        return xtile >= GRIDRANGEX[0] && xtile + tileEdgex <= GRIDRANGEX[1]
            && ytile >= GRIDRANGEY[0] && ytile + tileEdgey <= GRIDRANGEY[1];
    }

    public static int[] lowerLeftCorner()
    {
        return new int[] { GRIDRANGEX[0], GRIDRANGEY[0] };
    }

    public static int[] topLeftCorner()
    {
        return new int[] { GRIDRANGEX[0], GRIDRANGEY[1] };
    }

    /// <summary>
    /// Will return a character that represents the edge of the screen has been hit and where it has a been hit
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public static int inScreenBounds(Vector2 position)
    {
        if (position[0] < GRIDRANGEX[0] - 3)
            return 0;

        if (position[0] > GRIDRANGEX[1] + 3)
            return 1;

        if (position[1] < GRIDRANGEY[0] - 3)
            return 2;

        if (position[1] > GRIDRANGEY[1] + 3)
            return 3;

        return -1;
  
    }

    private Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();

    /// <summary>
    /// Gets the key for a corropsonding location to retrieve the appropriate tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static string getKey(int x, int y)
    {
        return (x.ToString() + " " +  y.ToString());
    }

    /// <summary>
    /// Generates and renders the whole map, then stores the tiles in the tile dictionary
    /// </summary>
    private void renderGrid()
    {
        //renders all the tiles
        for(int x = GRIDRANGEX[0] ; x <= GRIDRANGEX[1] ; x++)
        {
            for(int y = GRIDRANGEY[0]; y <= GRIDRANGEY[1]; y++)
            {
                //Debug.Log(getKey(x,y));
                Tile tile = gameObject.AddComponent<Tile>();
                tile.givePrefab(tilePrefab);
                tile.setPosition(x, y);
                tile.render();
                tiles.Add(getKey(x, y), tile);
            }
        }

    }

    // Use this for initialization
    void Start () {
        renderGrid();
	}
	
	// Update is called once per frame
	void Update () {
	}
}
