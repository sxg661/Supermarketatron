using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnitPlacement : MonoBehaviour
{

    private static Dictionary<string, UnitInfo> tiles = new Dictionary<string, UnitInfo>();
    private static Dictionary<int, UnitBehaviour> units = new Dictionary<int, UnitBehaviour>();

    public static int[] SHELFDIMENSIONS = new int[] { 2, 1 };
    public static int[] SHELFFLOORDIMENSIONS = new int[] { 2, 1 };
    public static int[] COUNTERDIMENSIONS = new int[] { 4, 2 };

    public UnitPrefabs unitPrefabs;

    private static int nextID = 0;

    private string mySceneName;

    public static int[] counterLoc = new int[] { 0, 0 };

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
        //Debug.Log("i am called");
        int[] coCoords = TileGrid.topLeftCorner();
        //InstantiateCounterUnit(coCoords[0] + 1, coCoords[1] - 2);
        counterLoc = TileGrid.topLeftCorner();

        for (int x = TileGrid.GRIDRANGEX[0]; x <= TileGrid.GRIDRANGEX[1]; x++)
        {
            for (int y = TileGrid.GRIDRANGEY[0]; y <= TileGrid.GRIDRANGEY[1]; y++)
            {
                if (tiles.ContainsKey(TileGrid.getKey(x, y)))
                {
                    //Debug.Log("there is a thing here");
                    UnitInfo currentTile = tiles[TileGrid.getKey(x, y)];
                    if(currentTile.innerX == 0 && currentTile.innerY == 0)
                    {
                        //Debug.Log("and here!");
                        switch (currentTile.type)
                        {
                            case UnitInfo.unitType.SHELF:
                                //Debug.Log("render");
                                InstantiateShelfUnit(x, y, staticUnit, currentTile.forward, false);
                                break;
                        }
                    }
                }
            }
        }
    }
   

    public void InstantiateShelfUnit(int blx, int bly, bool isStatic, bool forward, bool grabbed)
    {
        //get an ID for our new shelf
        int id = getNewID();

        //make a stock button for the shelf
        GameObject stockButton = Instantiate(unitPrefabs.stockPrefab, new Vector3(-100, -100, -100), Quaternion.identity);


        //make out new shelf using the right prefab and get the appropriate movement component
        Vector3 shelfPos = new Vector3(
                Tile.getPos(blx, SHELFDIMENSIONS[0]), Tile.getPos(bly, SHELFDIMENSIONS[1]), 0);

        UnitBehaviour movement;
        GameObject shelf;

        if (!isStatic)
        {
            shelf = Instantiate(unitPrefabs.shelfPrefab, shelfPos, Quaternion.identity);
            movement = shelf.GetComponent<UnitMovement>();

        }
        else
        {
            shelf = Instantiate(unitPrefabs.staticShelfPrefab, shelfPos, Quaternion.identity);
            movement = shelf.GetComponent<UnitStatic>();
        }
   

        //get the controller of the stock button
        ButtonController buttonController = stockButton.GetComponent<ButtonController>();


        //set all relevant parametres for this shelf
        movement.setId(id);
        movement.bleftx = blx;
        movement.blefty = bly;
        movement.myType = UnitInfo.unitType.SHELF;
        movement.setFloorLength(SHELFFLOORDIMENSIONS[0], SHELFFLOORDIMENSIONS[1]);
        movement.setSize(SHELFDIMENSIONS[0], SHELFDIMENSIONS[1]);
        movement.forward = forward;
        buttonController.setID(id);

        //give the movment it's stock button
        movement.giveStockButton(stockButton);

        //if it is grabbed we say the shelf is grabbed so it follows the cursor 
        //otherwise we add it in the grid so it cannot be overriden
        if (grabbed)
        {
            movement.grab();
           
        }
        else
        {
            addUnit(
                   blx, bly, id, SHELFFLOORDIMENSIONS[0], SHELFFLOORDIMENSIONS[1],
                   forward, UnitInfo.unitType.SHELF);
        }

        //put this in the dictionary so it can later be destroyed? may be redunant and can just do this from object movement?
        units[id] = movement;

    }


    /*
    public void  instantiateCounterUnit()
    {
        //instantiate unit
        Unit unit = gameObject.AddComponent<Unit>();
        unit.giveType(UnitInfo.unitType.COUNTER, unitPrefabs, true);
        int id = getNewID();
        unit.initStaticComponents(id);
        unit.givePosition(blx, bly, true);
        counterLoc = new int[] { blx, bly - 1 };

        //add to the dictionary
        units[id] = unit;
        //now do the rest
        addUnit(
            blx, bly, id, Unit.COUNTERDIMENSIONS[0], Unit.COUNTERDIMENSIONS[1],
            true, UnitInfo.unitType.COUNTER);
        unit.render(false);
    }*/

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


    public static bool canAccessShelf(int x, int y)
    {


        if (tiles.ContainsKey(TileGrid.getKey(x, y + 1)))
        {
            if(tiles[TileGrid.getKey(x, y + 1)].forward 
                && tiles[TileGrid.getKey(x, y + 1)].type == UnitInfo.unitType.SHELF)
            {
                return true;

            }
        }

        if (tiles.ContainsKey(TileGrid.getKey(x, y - 1)))
        {
            if(!tiles[TileGrid.getKey(x, y - 1)].forward
                && tiles[TileGrid.getKey(x, y - 1)].type == UnitInfo.unitType.SHELF)
            {
                return true;
            }
        }

        return false;
    }
}
