using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPrefabs : MonoBehaviour {

    public GameObject shelfPrefab;

    public GameObject stockPrefab;

    public GameObject getShelfPrefab()
    {
        return shelfPrefab;
    }

    public GameObject getStockPrefab()
    {
        return stockPrefab;
    }
    
}
