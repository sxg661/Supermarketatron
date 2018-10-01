using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController{

    private static bool stockMenuDisplayed = false;
    private static bool sellUnit = false;
    
    private static Optional<int> cursorObjID = Optional<int>.empty(); 

    public static Optional<int> stockUnitID = Optional<int>.empty();

    public static void setID(int ID)
    {
        stockUnitID = Optional<int>.of(ID);
    }


    public static Optional<int> getID()
    {
        return stockUnitID;
    }

    public static void pickUp(int id)
    {
        cursorObjID = Optional<int>.of(id);
    }

    public static void drop()
    {
        cursorObjID = Optional<int>.empty();
    }

    public static void sell()
    {
        if (cursorObjID.isPresent())
        {
            UnitPlacement.destroyUnit(cursorObjID.get());
        }
        UnitPlacement.freezeUnits(false);
    }

    public static void displayStockMenu()
    {
        stockMenuDisplayed = true;
        //Debug.Log("displaying menu");
    }

    public static void closeStockMenu()
    {
        stockMenuDisplayed = false;
    }

    public static bool displayingStockMenu()
    {
        return stockMenuDisplayed;
    }
}
