using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    private GameObject myPrefab;

    private int x;
    private int y;

    /// <summary>
    /// Gets the rendering postition of a certain tile
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static float getPos(int n, float sideLength)
    {
        return n + sideLength/2;
    }

    /// <summary>
    /// Gets the tile of certain rendering position
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int getTile(float n)
    {
        return (int) Mathf.Floor(n);
    }

    /// <summary>
    /// Passes a prefab for rendering into the tile
    /// </summary>
    /// <param name="prefab"></param>
    public void givePrefab(GameObject prefab)
    {
        myPrefab = prefab;
    }

    /// <summary>
    /// Sets the grid position of the tile
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void setPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    /// <summary>
    /// Renders the tile on the screen by instantiating the tile objects
    /// </summary>
    public void render()
    {
        if(myPrefab != null)
        {
            Vector3 location = new Vector3(getPos(x, 1f), getPos(y, 1f), 0);
            Instantiate(myPrefab, location, Quaternion.identity);
        }
    }

 



}
