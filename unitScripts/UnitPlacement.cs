using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitPlacement : MonoBehaviour
{

    private static Dictionary<string, UnitInfo> tiles = new Dictionary<string, UnitInfo>();
    private static Dictionary<int, Unit> units = new Dictionary<int, Unit>();

    public UnitPrefabs unitPrefabs;

    private static int nextID = 0;

    private string mySceneName;

    private static bool unitsFrozen = false;

    public static void freezeUnits(bool freeze)
    {
        unitsFrozen = freeze;
    }

    public static bool unitsAreFrozen()
    {
        return unitsFrozen;
    }

    public static int getNewID()
    {
        return nextID++;
    }

    public void initialiseUnits(bool staticUnit)
    {
        int[] coCoords = TileGrid.topLeftCorner();
        instantiateCounterUnit(coCoords[0] + 1, coCoords[1] - 2);


        for (int x = TileGrid.GRIDRANGEX[0]; x <= TileGrid.GRIDRANGEX[1]; x++)
        {
            for (int y = TileGrid.GRIDRANGEY[0]; y <= TileGrid.GRIDRANGEY[1]; y++)
            {
                if (tiles.ContainsKey(TileGrid.getKey(x, y)))
                {
                    UnitInfo currentTile = tiles[TileGrid.getKey(x, y)];
                    if(currentTile.innerX == 0 && currentTile.innerY == 0)
                    {
                        switch(currentTile.type)
                        {
                            case UnitInfo.unitType.SHELF:
                                instantiateShelfUnit(x, y, false, currentTile.forward, staticUnit);
                                break;
                        }
                    }
                }
            }
        }
    }

    public void instantiateShelfUnit(int blx, int bly, bool pending, bool forward, bool staticUnit)
	{
        Unit unit = gameObject.AddComponent<Unit>();

        //for now I only have shelves, but I'll change this when I get there
        unit.giveType(UnitInfo.unitType.SHELF, unitPrefabs, staticUnit);
        int id = getNewID();
        if (staticUnit)
        {
            unit.initStaticComponents(id);
        }
        else
        {
            unit.initMovingComponents(id);
        }
            
        //HERE
        unit.givePosition(blx, bly, forward);

        //we add this to our dictionary so we can destroy it later if need be
        units[id] = unit;
        //maybe need to change how I do this too, so can generalise function for all unit types
        if(!pending)
            addUnit(
                blx, bly, id, Unit.SHELFFLOORDIMENSIONS[0], Unit.SHELFFLOORDIMENSIONS[1], 
                forward, UnitInfo.unitType.SHELF);

        unit.render(pending);

    }

    public void instantiateCounterUnit(int blx, int bly)
    {
        //instantiate unit
        Unit unit = gameObject.AddComponent<Unit>();
        unit.giveType(UnitInfo.unitType.COUNTER, unitPrefabs, true);
        int id = getNewID();
        unit.initStaticComponents(id);
        unit.givePosition(blx, bly, true);

        //add to the dictionary
        units[id] = unit;
        //now do the rest
        addUnit(
            blx, bly, id, Unit.COUNTERDIMENSIONS[0], Unit.COUNTERDIMENSIONS[1], 
            true, UnitInfo.unitType.COUNTER);
        unit.render(false);
      
    }

    public void Start()
    {
        unitsFrozen = false;
        mySceneName = SceneManager.GetActiveScene().name;

        //units only move in the build scene
        if (mySceneName == "BuildScene")
        {
            initialiseUnits(false);
        }
        else initialiseUnits(true);
    }


    public static void destroyUnit(int id)
    {
        units[id].destroyMe();
        units.Remove(id);
    }

    /// <summary>
    /// Adds a shelf unit to the units going by its bottom left coordinate
    /// </summary>
    /// <param name="blx">Bottom left coordiante of shelf</param>
    /// <param name="bly">Bottom right coordinate of shelf</param>
    public static void addUnit(int blx, int bly, int myID, float xsize, float ysize, bool forward, UnitInfo.unitType type)
    {


        for(int x = 0; x < xsize; x++)
        {
            for(int y = 0; y < ysize; y++)
            {
                tiles[TileGrid.getKey(blx + x, bly + y)] = 
                    new UnitInfo(type, myID, x, y, forward);
            }
        }
         
    }

    /// <summary>
    /// Removes a shelf unit to the units going by its bottom left coordinate
    /// </summary>
    /// <param name="blx">Bottom left coordiante of shelf</param>
    /// <param name="bly">Bottom right coordinate of shelf</param>
    public static void removeUnit(int blx, int bly, float xsize, float ysize)
    {
        //Again, no need for bound checking as it is done for us by this point

        //Debug.Log("removing a shelf : " +  blx + " " + bly);

        for (int x = 0; x < xsize; x++)
        {
            for (int y = 0; y < ysize; y++)
            {
                tiles.Remove(TileGrid.getKey(blx + x, bly + y));
            }
        }
    }

    /// <summary>
    /// Checks a square to see if it is occupied on the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool occupied(int x, int y, int myID)
    {
        string keyToCheck = TileGrid.getKey(x, y);
        //Debug.Log("Checking Key " + keyToCheck);
        //Debug.Log(tiles.ContainsKey(keyToCheck));

        if (tiles.ContainsKey(keyToCheck))
            //it is allowed to collide with itself
            return tiles[keyToCheck].getID() != myID;

        return false;
    }
}
