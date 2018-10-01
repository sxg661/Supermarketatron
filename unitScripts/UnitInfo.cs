using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo
{
    public enum unitType{
        SHELF
    }

    private int id;

    public unitType type;

    public bool forward;

    public int innerX, innerY;

    //units usually go over several tiles, so this gives the position
    //with in the unit, starting from the bottom left tile
    private Vector2 unitCoords;

    public UnitInfo(unitType unitType, int myID, int innerX, int innerY, bool forward)
    {
        type = unitType;
        id = myID;
        this.forward = forward;
        this.innerX = innerX;
        this.innerY = innerY;
    }

    public int getID()
    {
        return id;
    }

}
