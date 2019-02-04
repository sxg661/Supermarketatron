using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour {

    public int numCustomers = 0;

    private int capacity = TileGrid.getGridSize() / 12;

    public GameObject customerPrefab;

    //higher means less probable
    private int probOfCust = 200;

    public void removeCustomer()
    {
        numCustomers--;
    }

    public Vector3 entrance;

	// Use this for initialization
	void Start () {
        int[] ent = TileGrid.getDoorLoc();
        entrance = new Vector3(Tile.getPos(ent[0],1), Tile.getPos(ent[1],1), 0);

	}
	
	// Update is called once per frame
	void Update () {
		if(numCustomers < capacity && Random.Range(0,probOfCust) == 0)
        {
            GameObject newCust = Instantiate(customerPrefab, entrance, Quaternion.identity);
            CasualCustomer custControl = newCust.GetComponent<CasualCustomer>();
            custControl.spawner = this;
            numCustomers++;
        }
	}
}
