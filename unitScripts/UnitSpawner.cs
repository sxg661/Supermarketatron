using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    public GameObject unitPlacer;
    private UnitPlacement unitPlacement;


    // Use this for initialization
    void Start () {
        unitPlacement = unitPlacer.GetComponent<UnitPlacement>();
    }
	
    public void spawnUnit()
    {
        int[] spawnPos = TileGrid.lowerLeftCorner();
        unitPlacement.InstantiateShelfUnit(spawnPos[0], spawnPos[1], false, true, true);
    }
}
